import styles from '@app/styles'
import React from 'react'
import { View, Text } from 'react-native'

interface props {
  icon: React.ReactElement
  value: string
  unit: string
}

const InfoItem = ({ icon, unit, value }: props): React.ReactElement => {
  return (
    <View style={[styles.justifyCenter, styles.alignCenter]}>
      <Text style={[styles.textLg, styles.mb20, styles.textWhite]}>{value} {unit}</Text>
      {icon}
    </View>
  )
}

export default InfoItem
