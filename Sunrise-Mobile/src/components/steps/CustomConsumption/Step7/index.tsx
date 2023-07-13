import PrimaryButton from '@app/components/general/PrimaryButton'
import useStepper from '@app/hooks/useStepper'
import { getEstimateConso } from '@app/services/stepper.service'
import styles from '@app/styles'
import colors from '@app/styles/colors'
import React from 'react'
import { ActivityIndicator, Text, View } from 'react-native'
import MaskInput from 'react-native-mask-input'
import { useQuery } from 'react-query'

const Step7CustomConsumption = (): React.ReactElement => {
  const { setStep7 } = useStepper()
  const { isLoading } = useQuery('estimation', getEstimateConso, {
    onSuccess: (d) => { setEstimation(d.toString()) }
  })
  const [estimation, setEstimation] = React.useState<string>('0')

  return (
    <View style={[styles.flex1, styles.alignCenter]}>
      <Text style={[styles.textXl, styles.textGray, styles.textCenter]}>Consommation estimée par jour en KW/H</Text>
      <View style={[styles.justifyEvenly, styles.alignCenter, styles.flex1, styles.w80]}>
        {isLoading
          ? <ActivityIndicator size='large' color={colors.primary} />
          : <MaskInput
            style={[styles.bigInput, styles.w100]}
            keyboardType='number-pad'
            mask={[/\d/, /\d/]}
            value={estimation}
            onChangeText={(e) => { setEstimation(e) }}
          />}

        <PrimaryButton title='Finaliser' style={styles.w100} onPress={() => { setStep7(parseInt(estimation)) }} />
      </View>
    </View>
  )
}

export default Step7CustomConsumption
