/* eslint-disable @typescript-eslint/no-confusing-void-expression */
import { type DeviceResponse } from '@app/core/interfaces/device.interface'
import { type FormEnedisSetting, type ConsumptionStepper } from '@app/core/interfaces/forms.interface'
import { type AuthUser } from '@app/core/interfaces/user.interface'
import myFetch from '@app/core/utils/myFetching'

export const getStepperData = async (): Promise<DeviceResponse> => {
  return await myFetch.get<DeviceResponse>('consumption/stepperdata').json()
}

export const setSettings = async (form: ConsumptionStepper): Promise<void> => {
  // eslint-disable-next-line @typescript-eslint/no-invalid-void-type
  return await myFetch.post<void>('consumption/setsettings', form).json()
}

export const getEstimateConso = async (): Promise<number> => {
  return await myFetch.get<number>('consumption/estimation').json()
}

export const setEstimation = async (estimation: number): Promise<AuthUser> => {
  return await myFetch.post<AuthUser>('consumption/setestimation', estimation).json()
}

export const setEnedisSettings = async (formData: FormEnedisSetting): Promise<AuthUser> => {
  return await myFetch.post<AuthUser>('enedisconsumption/setsettings', formData).json()
}
