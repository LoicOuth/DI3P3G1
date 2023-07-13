import { type IDevice } from '@app/core/interfaces/device.interface'
import React from 'react'
import styles from '@app/styles'
import { itemListStyle } from './style'
import { View, Text } from 'react-native'

interface props {
  content: string
  devices?: IDevice[]
}

const ItemList = ({ content, devices }: props): React.ReactElement => {
  return (
    <View style={[itemListStyle.container]}>
      <View style={[styles.flexRow, styles.alignCenter]}>
        <View style={[itemListStyle.bluePoint]} />
        <Text style={[itemListStyle.textContent, itemListStyle.marginLeft]}>
          {content} {devices && devices.length > 0 && ' :'}
        </Text>
      </View>
      {devices && devices.length > 0 &&
      <View style={[itemListStyle.marginLeft, styles.flexColumn]}>
        {devices.map((device, index) => (
          <View key={index} style={[styles.flexRow, styles.alignCenter, styles.justifyCenter]}>
            <View style={[itemListStyle.blackPoint]} />
            <Text style={[itemListStyle.marginLeft, itemListStyle.textContent, itemListStyle.widthList]}>{device.name}</Text>
          </View>
        ))}
      </View>
      }
    </View>
  )
}

export default ItemList
