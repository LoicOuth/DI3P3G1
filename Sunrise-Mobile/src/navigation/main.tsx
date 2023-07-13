import React from 'react'
import { NavigationContainer } from '@react-navigation/native'
import AppNavigator from './app'
import AuthNavigator from './auth'
import useAuth from '@app/hooks/useAuth'
import SplashScreen from '@app/components/general/SplashScreen'
import Stepper from '@app/screens/Stepper'
import { StepperProvider } from '@app/providers/stepper.provider'
import ScanNavigator from './scan'

const Router = (): React.ReactElement => {
  const { authData, isLoading } = useAuth()

  if (isLoading) {
    return <SplashScreen />
  }

  if (authData?.user?.consumption === null && authData?.user?.pdm === null) {
    return (
      <StepperProvider>
        <Stepper />
      </StepperProvider>
    )
  }

  if (authData?.user?.deviceId === null) {
    return (
      <NavigationContainer>
        <ScanNavigator />
      </NavigationContainer>
    )
  }

  return (
    <NavigationContainer>
      {authData?.isLogin ? <AppNavigator /> : <AuthNavigator />}
    </NavigationContainer>
  )
}

export default Router
