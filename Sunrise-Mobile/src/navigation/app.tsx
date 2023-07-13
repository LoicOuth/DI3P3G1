import React from 'react'
import { createBottomTabNavigator } from '@react-navigation/bottom-tabs'
import Icon from 'react-native-vector-icons/FontAwesome'
import DashboardScreen from '../screens/Dashboard'
import ProfileScreen from '../screens/Profile'
import ProfileNavigator from '@app/navigation/profile'
import ConsumerHabitScreen from '@app/screens/ConsumerHabit'
import colors from '@app/styles/colors'

const AppNavigator = (): React.ReactElement => {
  const Tab = createBottomTabNavigator()

  return (
    <Tab.Navigator
      screenOptions={{
        headerShown: false,
        tabBarShowLabel: false,
        tabBarIconStyle: {
          color: colors.primary
        }
      }}
    >
      <Tab.Screen
        name="Dashboard"
        component={DashboardScreen}
        options={{
          tabBarIcon: ({ color, size }) => (
            <Icon name="dashboard" color={color} size={size} />
          )
        }}
      />
      <Tab.Screen
        name="ConsumerHabit"
        component={ConsumerHabitScreen}
        options={{
          tabBarIcon: ({ color, size }) => (
            <Icon name="home" color={color} size={size} />
          )
        }}
      />
      <Tab.Screen
        name="Profile"
        component={ProfileScreen}
        options={{
          tabBarIcon: ({ color, size }) => (
            <Icon name="user" color={color} size={size} />
          )
        }}
      />
      <Tab.Screen
        name='Step'
        component={ProfileNavigator}
        initialParams={{ isVisible: false }}
        options={{
          tabBarButton: () => null
        }}
      />
    </Tab.Navigator>
  )
}

export default AppNavigator
