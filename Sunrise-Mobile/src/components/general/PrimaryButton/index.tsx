import React from 'react'
import styles from '@app/styles'
import { ActivityIndicator, type StyleProp, Text, TouchableOpacity } from 'react-native'
import { BtnSize } from '@app/core/enums/btnSize.enum'
import colors from '@app/styles/colors'

interface props {
  onPress: () => void
  title: string
  isLoading?: boolean
  style?: StyleProp<any>
  size?: BtnSize
  color?: string
  textColor?: string
}

const PrimaryButton = ({ onPress, title, style, textColor = 'white', color = colors.primary, size = BtnSize.BIG, isLoading = false }: props): React.ReactElement => {
  const getSize = (): StyleProp<any> => {
    /* eslint-disable indent */
    switch (size) {
      case BtnSize.BIG:
        return styles.textXl

      case BtnSize.SMALL:
        return styles.textLg
    }
  }

  return (
      <TouchableOpacity
          style={[styles.btn, isLoading ? styles.opactity5 : null, style, { backgroundColor: color }]}
          disabled={isLoading}
          onPress={onPress}
      >
        {isLoading
          ? <ActivityIndicator color="white" size="large" />
          : <Text style={[getSize(), { color: textColor }, styles.textCenter]}>{title}</Text>
        }
      </TouchableOpacity>
  )
}

export default PrimaryButton
