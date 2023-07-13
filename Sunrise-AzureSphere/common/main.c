/* Copyright (c) Microsoft Corporation. All rights reserved.
   Licensed under the MIT License. */

// This sample C application demonstrates how to use Azure Sphere devices with Azure IoT
// services, using the Azure IoT C SDK.
//
// It implements a simulated thermometer device, with the following features:
// - Telemetry upload (simulated temperature, device moved events) using Azure IoT Hub events.
// - Reporting device state (serial number) using device twin/read-only properties.
// - Mutable device state (telemetry upload enabled) using device twin/writeable properties.
// - Alert messages invoked from the cloud using device methods.
//
// It can be configured using the top-level CMakeLists.txt to connect either directly to an
// Azure IoT Hub, to an Azure IoT Edge device, or to use the Azure Device Provisioning service to
// connect to either an Azure IoT Hub, or an Azure IoT Central application. All connection types
// make use of the device certificate issued by the Azure Sphere security service to authenticate,
// and supply an Azure IoT PnP model ID on connection.
//
// It uses the following Azure Sphere libraries:
// - eventloop (system invokes handlers for timer events)
// - gpio (digital input for button, digital output for LED)
// - log (displays messages in the Device Output window during debugging)
// - networking (network interface connection status)
//
// You will need to provide information in the application manifest to use this application. Please
// see README.md and the other linked documentation for full details.

#include <ctype.h>
#include <errno.h>
#include <signal.h>
#include <stdbool.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

// applibs_versions.h defines the API struct versions to use for applibs APIs.
#include "applibs_versions.h"
#include <applibs/eventloop.h>
#include <applibs/networking.h>
#include <applibs/log.h>
#include <applibs/adc.h>
#include <applibs/gpio.h>

#include "eventloop_timer_utilities.h"
#include "user_interface.h"
#include "exitcodes.h"
#include "cloud.h"
#include "options.h"
#include "connection.h"
#include <hw/sample_appliance.h>

static volatile sig_atomic_t exitCode = ExitCode_Success;
static int adcControllerFd = -1;
static float sampleMaxVoltage = 2.0f;
static int sampleBitCount = -1;
static int sleepRead = 10;
static int sleepChauffeEau = 1;
static int gpioFd = -1;

// Initialization/Cleanup
static ExitCode InitPeripheralsAndHandlers(void);
static void ClosePeripheralsAndHandlers(void);

// Interface callbacks
static void ExitCodeCallbackHandler(ExitCode ec);
static void ButtonPressedCallbackHandler(UserInterface_Button button);

// Cloud
static const char *CloudResultToString(Cloud_Result result);
static void ConnectionChangedCallbackHandler(bool connected);
static void CloudChauffeEauEnabledChangedCallbackHandler(bool status, bool fromCloud);
static void DisplayAlertCallbackHandler(const char *alertMessage);

// Timer / polling
static bool isConnected = false;

static EventLoop *eventLoop = NULL;
static EventLoopTimer *telemetryTimer = NULL;


static EventLoop *eventLoopChauffeEau = NULL;
static EventLoopTimer *chauffeEauTimer = NULL;

// Business logic
static void DeviceMoved(void);
static void SetChauffeEauEnabled(bool uploadEnabled, bool fromCloud);
static bool chauffeEauEnabled = false; // False by default - do not send telemetry until told
                                            // by the user or the cloud

static const char *serialNumber = "TEMPMON-01234";

/// <summary>
///     Signal handler for termination requests. This handler must be async-signal-safe.
/// </summary>
static void TerminationHandler(int signalNumber)
{
    // Don't use Log_Debug here, as it is not guaranteed to be async-signal-safe.
    exitCode = ExitCode_TermHandler_SigTerm;
}

/// <summary>
///     Main entry point for this sample.
/// </summary>
int main(int argc, char *argv[])
{
    Log_Debug("Azure IoT Application starting.\n");

    bool isNetworkingReady = false;
    if ((Networking_IsNetworkingReady(&isNetworkingReady) == -1) || !isNetworkingReady) {
        Log_Debug("WARNING: Network is not ready. Device cannot connect until network is ready.\n");
    }

    exitCode = Options_ParseArgs(argc, argv);

    if (exitCode != ExitCode_Success) {
        return exitCode;
    }

    exitCode = InitPeripheralsAndHandlers();

    // Main loop
    while (exitCode == ExitCode_Success) {
        EventLoop_Run_Result result = EventLoop_Run(eventLoop, -1, true);
        // Continue if interrupted by signal, e.g. due to breakpoint being set.
        if (result == EventLoop_Run_Failed && errno != EINTR) {
            exitCode = ExitCode_Main_EventLoopFail;
        }
        EventLoop_Run_Result resultChauffeEau = EventLoop_Run(eventLoopChauffeEau, -1, true);
        if (resultChauffeEau == EventLoop_Run_Failed && errno != EINTR) {
            exitCode = ExitCode_Main_EventLoopFail;
        }
    }

    ClosePeripheralsAndHandlers();

    Log_Debug("Application exiting.\n");

    return exitCode;
}

static void ExitCodeCallbackHandler(ExitCode ec)
{
    exitCode = ec;
}

static const char *CloudResultToString(Cloud_Result result)
{
    switch (result) {
    case Cloud_Result_OK:
        return "OK";
    case Cloud_Result_NoNetwork:
        return "No network connection available";
    case Cloud_Result_OtherFailure:
        return "Other failure";
    }

    return "Unknown Cloud_Result";
}

static void SetChauffeEauEnabled(bool uploadEnabled, bool fromCloud)
{
    chauffeEauEnabled = uploadEnabled;
    UserInterface_SetStatus(uploadEnabled);

    Cloud_Result result =
        Cloud_SendChauffeEauEnabledChangedEvent(uploadEnabled, fromCloud);
    if (result != Cloud_Result_OK) {
        Log_Debug(
            "WARNING: Could not send thermometer telemetry upload enabled changed event to cloud: "
            "%s\n",
            CloudResultToString(result));
    }
}

static void DeviceMoved(void)
{
    Log_Debug("INFO: Device moved.\n");

    time_t now;
    time(&now);

    Cloud_Result result = Cloud_SendThermometerMovedEvent(now);
    if (result != Cloud_Result_OK) {
        Log_Debug("WARNING: Could not send thermometer moved event to cloud: %s\n",
                  CloudResultToString(result));
    }
}

static void ButtonPressedCallbackHandler(UserInterface_Button button)
{
    if (button == UserInterface_Button_A) {
        bool newchauffeEauEnabled = !chauffeEauEnabled;
        Log_Debug("INFO: Telemetry upload enabled state changed (via button press): %s\n",
                  newchauffeEauEnabled ? "enabled" : "disabled");
        SetChauffeEauEnabled(newchauffeEauEnabled, false);
    } else if (button == UserInterface_Button_B) {
        DeviceMoved();
    }
}

static void CloudChauffeEauEnabledChangedCallbackHandler(bool uploadEnabled, bool fromCloud)
{
    Log_Debug("INFO: Chauffe eau state changed (via cloud): %s\n",
              uploadEnabled ? "enabled" : "disabled");
    SetChauffeEauEnabled(uploadEnabled, fromCloud);
}

static void DisplayAlertCallbackHandler(const char *alertMessage)
{
    Log_Debug("ALERT: %s\n", alertMessage);
}

static void ConnectionChangedCallbackHandler(bool connected)
{
    isConnected = connected;

    if (isConnected) {
        Cloud_Result result = Cloud_SendDeviceDetails(serialNumber);
        if (result != Cloud_Result_OK) {
            Log_Debug("WARNING: Could not send device details to cloud: %s\n",
                      CloudResultToString(result));
        }
    }
}

static void TelemetryTimerCallbackHandler(EventLoopTimer *timer)
{
    static Cloud_Telemetry telemetry = {.voltage = 0.f};

    if (ConsumeEventLoopTimerEvent(timer) != 0) {
        exitCode = ExitCode_TelemetryTimer_Consume;
        return;
    }

    time_t now;
    time(&now);

    if (isConnected) {
        //Send data to cloud
        uint32_t value;
        int poll_value = ADC_Poll(adcControllerFd, SAMPLE_POTENTIOMETER_ADC_CHANNEL, &value);
        if (poll_value == -1) {
            Log_Debug("ADC_Poll failed with error: %s (%d)\n", strerror(errno), errno);
            exitCode = ExitCode_AdcTimerHandler_Poll;
            return;
        }
        float voltage = ((float)value * sampleMaxVoltage) / (float)((1 << sampleBitCount) - 1);
        voltage = voltage * 100 / sampleMaxVoltage; 
        telemetry.voltage = voltage;
        
        Cloud_Result result = Cloud_SendTelemetry(&telemetry, now);
        if (result != Cloud_Result_OK) {
            Log_Debug("WARNING: Could not send telemetry to cloud: %s\n",
                    CloudResultToString(result));
        }
    } else {
        Log_Debug("INFO: Can't send telemtry because the IoT Hub connection is not started yet.\n");
    }
}

static void ChauffeEauTimerCallbackHandler(EventLoopTimer *timer)
{
    if (ConsumeEventLoopTimerEvent(timer) != 0) {
        exitCode = ExitCode_TelemetryTimer_Consume;
        return;
    }
    time_t now;
    time(&now);

    if (isConnected) {
        //Update enable chauffe eau
        if (chauffeEauEnabled) 
        {
            GPIO_SetValue(gpioFd, GPIO_Value_High);
        } else {
            GPIO_SetValue(gpioFd, GPIO_Value_Low);
        }
    }
}
/// <summary>
///     Set up SIGTERM termination handler, initialize peripherals, and set up event handlers.
/// </summary>
/// <returns>
///     ExitCode_Success if all resources were allocated successfully; otherwise another
///     ExitCode value which indicates the specific failure.
/// </returns>
static ExitCode InitPeripheralsAndHandlers(void)
{
    struct sigaction action;
    memset(&action, 0, sizeof(struct sigaction));
    action.sa_handler = TerminationHandler;
    sigaction(SIGTERM, &action, NULL);

    eventLoop = EventLoop_Create();
    if (eventLoop == NULL) {
        Log_Debug("Could not create event loop.\n");
        return ExitCode_Init_EventLoop;
    }
    eventLoopChauffeEau = EventLoop_Create();
    if (eventLoopChauffeEau == NULL) {
        Log_Debug("Could not create event loop chauffe eau.\n");
        return ExitCode_Init_EventLoop;
    }
    gpioFd = GPIO_OpenAsOutput(SAMPLE_LED_PIN, GPIO_OutputMode_PushPull,
                               GPIO_Value_Low);
    if (gpioFd < 0) {
        Log_Debug("ERROR: Could not open button GPIO: %s (%d).\n", strerror(errno), errno);
        return ExitCode_Init_OpenGpio;
    }
    adcControllerFd = ADC_Open(SAMPLE_POTENTIOMETER_ADC_CONTROLLER);
    if (adcControllerFd == -1) {
        Log_Debug("ADC_Open failed with error: %s (%d)\n", strerror(errno), errno);
        return ExitCode_Init_AdcOpen;
    }

    sampleBitCount = ADC_GetSampleBitCount(adcControllerFd, SAMPLE_POTENTIOMETER_ADC_CHANNEL);
    if (sampleBitCount == -1) {
        Log_Debug("ADC_GetSampleBitCount failed with error : %s (%d)\n", strerror(errno), errno);
        return ExitCode_Init_GetBitCount;
    }
    if (sampleBitCount == 0) {
        Log_Debug("ADC_GetSampleBitCount returned sample size of 0 bits.\n");
        return ExitCode_Init_UnexpectedBitCount;
    }

    int result = ADC_SetReferenceVoltage(adcControllerFd, SAMPLE_POTENTIOMETER_ADC_CHANNEL,
                                         sampleMaxVoltage);
    if (result == -1) {
        Log_Debug("ADC_SetReferenceVoltage failed with error : %s (%d)\n", strerror(errno), errno);
        return ExitCode_Init_SetRefVoltage;
    }

    struct timespec telemetryPeriod = {.tv_sec = sleepRead, .tv_nsec = 0};
    telemetryTimer =
        CreateEventLoopPeriodicTimer(eventLoop, &TelemetryTimerCallbackHandler, &telemetryPeriod);
    if (telemetryTimer == NULL) {
        return ExitCode_Init_TelemetryTimer;
    }
    struct timespec chauffeEauPeriod = {.tv_sec = sleepChauffeEau, .tv_nsec = 0};
    chauffeEauTimer =
        CreateEventLoopPeriodicTimer(eventLoopChauffeEau, &ChauffeEauTimerCallbackHandler, &chauffeEauPeriod);
    if (chauffeEauTimer == NULL) {
        return ExitCode_Init_ChauffeEauTimer;
    }

    ExitCode interfaceExitCode =
        UserInterface_Initialise(eventLoop, ButtonPressedCallbackHandler, ExitCodeCallbackHandler);

    if (interfaceExitCode != ExitCode_Success) {
        return interfaceExitCode;
    }

    UserInterface_SetStatus(chauffeEauEnabled);

    void *connectionContext = Options_GetConnectionContext();

    return Cloud_Initialize(eventLoop, connectionContext, ExitCodeCallbackHandler,
                            CloudChauffeEauEnabledChangedCallbackHandler,
                            DisplayAlertCallbackHandler, ConnectionChangedCallbackHandler);
}

/// <summary>
///     Close peripherals and handlers.
/// </summary>
static void ClosePeripheralsAndHandlers(void)
{
    DisposeEventLoopTimer(telemetryTimer);
    DisposeEventLoopTimer(chauffeEauTimer);
    Cloud_Cleanup();
    UserInterface_Cleanup();
    Connection_Cleanup();
    EventLoop_Close(eventLoop);
    EventLoop_Close(eventLoopChauffeEau);

    Log_Debug("Closing file descriptors\n");
}
