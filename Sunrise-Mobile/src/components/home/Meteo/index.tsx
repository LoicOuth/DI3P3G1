import { type IMeteo } from '@app/core/interfaces/meteo.interface'
import styles from '@app/styles'
import React from 'react'
import { type ReactElement } from 'react'
import { View, Text, Image } from 'react-native'
import meteoStyle from './style'

interface props {
  _meteo: IMeteo
}

const Meteo = ({ _meteo }: props): ReactElement => {
  return (
    <View>
      <Image source={{ uri: _meteo.icon_big }} style={{ height: 100, width: 100, opacity: 0.6 }} />
      <Text style={[styles.textXl, styles.textWhite, styles.absolute, meteoStyle.text]}>{_meteo.tmp}°C</Text>
    </View>
  )
}

export default Meteo
