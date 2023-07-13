import { createNativeStackNavigator } from '@react-navigation/native-stack'
import React from 'react'
import LoginScreen from '../screens/auth/Login'

const AuthNavigator = (): React.ReactElement => {
  const Stack = createNativeStackNavigator()

  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="Auth.Login" component={LoginScreen} />
    </Stack.Navigator>
  )
}

export default AuthNavigator
