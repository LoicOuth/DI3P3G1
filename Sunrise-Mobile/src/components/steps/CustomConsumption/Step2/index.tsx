import PrimaryButton from '@app/components/general/PrimaryButton'
import useStepper from '@app/hooks/useStepper'
import styles from '@app/styles'
import React from 'react'
import { Text, View } from 'react-native'

const Step2CustomConsumption = (): React.ReactElement => {
  const { setStep2 } = useStepper()

  return (
    <View style={[styles.flex1, styles.alignCenter]}>
      <Text style={[styles.textXl, styles.textGray, styles.textCenter]}>Êtes-vous en télétravail ?</Text>
      <View style={[styles.flexRow, styles.alignCenter, styles.justifyEvenly, styles.w100, styles.flex1]}>
        <PrimaryButton title='Non'onPress={() => { setStep2(false) }} color='white' textColor='black' />
        <PrimaryButton title='Oui'onPress={() => { setStep2(true) }} />
      </View>
    </View>
  )
}

export default Step2CustomConsumption
