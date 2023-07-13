import ScanModule from '@app/screens/ScanModule'
import ValidId from '@app/screens/ScanModule/ValidId'
import { createNativeStackNavigator } from '@react-navigation/native-stack'
import React from 'react'

const ScanNavigator = (): React.ReactElement => {
  const Stack = createNativeStackNavigator()

  return (
    <Stack.Navigator screenOptions={{ headerShown: false }}>
      <Stack.Screen name="ScanModule" component={ScanModule} />
      <Stack.Screen name="ValidId" component={ValidId} />
    </Stack.Navigator>

  )
}

export default ScanNavigator
