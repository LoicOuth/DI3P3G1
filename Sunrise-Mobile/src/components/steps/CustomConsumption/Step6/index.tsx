import PrimaryButton from '@app/components/general/PrimaryButton'
import useStepper from '@app/hooks/useStepper'
import styles from '@app/styles'
import React from 'react'
import { Text, View } from 'react-native'
import MaskInput from 'react-native-mask-input'

const Step6CustomConsumption = (): React.ReactElement => {
  const { setStep6, data } = useStepper()
  const [nbMachine, setNbMachine] = React.useState(data.washingNumber ? data.washingNumber.toString() : '0')

  return (
    <View style={[styles.flex1, styles.alignCenter]}>
      <Text style={[styles.textXl, styles.textGray, styles.textCenter]}>Combien faites vous de machines à laver par semaine</Text>
      <View style={[styles.justifyEvenly, styles.alignCenter, styles.flex1, styles.w80]}>
        <MaskInput
          style={[styles.bigInput, styles.w100]}
          keyboardType='number-pad'
          mask={[/\d/, /\d/]}
          value={nbMachine}
          onChangeText={(e) => { setNbMachine(e) }}
        />

        <PrimaryButton title='Valider' style={styles.w100} onPress={() => { setStep6(parseInt(nbMachine)) }} />
      </View>
    </View>
  )
}

export default Step6CustomConsumption
