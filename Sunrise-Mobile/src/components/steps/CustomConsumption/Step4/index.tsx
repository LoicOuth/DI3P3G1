import useStepper from '@app/hooks/useStepper'
import styles from '@app/styles'
import React from 'react'
import { Text, View } from 'react-native'
import CheckBox from '../CheckBox'

const Step4CustomConsumption = (): React.ReactElement => {
  const { setStep4 } = useStepper()

  const getCheck = (): React.ReactElement[] => {
    const all: React.ReactElement[] = []

    for (let index = 1; index < 6; index++) {
      all.push(<CheckBox key={index} num={index} onUpdate={(data: number) => { setStep4(data) }} />)
    }

    return all
  }

  return (
    <View style={[styles.flex1, styles.alignCenter]}>
      <Text style={[styles.textXl, styles.textGray, styles.textCenter]}>Nombre de personnes au sein du foyer</Text>
      <View style={[styles.flexRow, { marginTop: 100 }]}>
        {getCheck()}
      </View>
    </View>
  )
}

export default Step4CustomConsumption
