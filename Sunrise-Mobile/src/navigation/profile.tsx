import { createNativeStackNavigator } from '@react-navigation/native-stack'
import React from 'react'
import StepperScreen from '../screens/Stepper'
import { StepperProvider } from '@app/providers/stepper.provider'

const ProfileNavigator = (): React.ReactElement => {
  const Stack = createNativeStackNavigator()

  return (
    <StepperProvider isNavigate={true}>
      <Stack.Navigator screenOptions={{ headerShown: false }}>
        <Stack.Screen name="ProfileToStepper" component={StepperScreen} />
      </Stack.Navigator>
    </StepperProvider>
  )
}

export default ProfileNavigator
