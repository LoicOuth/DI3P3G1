import React from 'react'
import { Image, Text, View } from 'react-native'
import splashScreenStyles from './style'

const IMAGE = require('../../../../assets/splash.png')

const SplashScreen = (): React.ReactElement => {
  return (
    <View>
      <Image source={IMAGE} style={{ height: '100%', width: '100%' }}/>
      <Text style={splashScreenStyles.splashText}>Chargement en cours...</Text>
    </View>
  )
}

export default SplashScreen
