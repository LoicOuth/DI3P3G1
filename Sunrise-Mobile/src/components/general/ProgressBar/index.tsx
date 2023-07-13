import React from 'react'
import { Animated, View } from 'react-native'
import { progressStyles } from './style'

interface props {
  max: number
  curr: number
}

const ProgressBar = ({ max, curr }: props): React.ReactElement => {
  const width = (): string => {
    return `${100 * curr / max}%`
  }

  return (
    <View style={progressStyles.progressContainer}>
      <Animated.View style={[progressStyles.progress, { width: width() }]}/>
    </View>
  )
}

export default ProgressBar
