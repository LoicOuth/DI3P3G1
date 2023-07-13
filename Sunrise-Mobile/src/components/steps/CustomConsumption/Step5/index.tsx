import { getStepperData } from '@app/services/stepper.service'
import styles from '@app/styles'
import React from 'react'
import { ActivityIndicator, Text, View } from 'react-native'
import { useQuery } from 'react-query'
import BouncyCheckbox from 'react-native-bouncy-checkbox'
import colors from '@app/styles/colors'
import PrimaryButton from '@app/components/general/PrimaryButton'
import useStepper from '@app/hooks/useStepper'

const Step5CustomConsumption = (): React.ReactElement => {
  const { data, setStep5 } = useStepper()
  const { isLoading, data: deviceResponse } = useQuery('stepperData', getStepperData)
  const deviceArray: string[] = data?.device ? data.device : []

  const onChange = (deviceId: string): void => {
    const index = deviceArray.indexOf(deviceId)

    if (index === -1) {
      deviceArray.push(deviceId)
    } else {
      deviceArray.splice(index, 1)
    }
  }

  return (
    <View style={[styles.flex1, styles.alignCenter]}>
      <Text style={[styles.textXl, styles.textGray, styles.textCenter]}>Choisissez les équipements que vous avez chez vous</Text>
      <View style={[styles.alignCenter, styles.w100, { marginTop: 30 }]}>
        <View>
          {isLoading
            ? <ActivityIndicator size='large' color={colors.primary} />
            : deviceResponse?.devices.map(el =>
              <BouncyCheckbox
                key={el.id}
                size={30}
                isChecked={deviceArray.some(device => device === el.id)}
                fillColor={colors.primary}
                unfillColor="#FFFFFF"
                text={el.name}
                style={{ marginTop: 20 }}
                iconStyle={{ borderColor: colors.primary }}
                textStyle={styles.textLg}
                innerIconStyle={{ borderWidth: 2 }}
                onPress={() => { onChange(el.id) }}
              />
            )
          }
        </View>
        <View style={[styles.w80, { marginTop: 50 }]}>
          <PrimaryButton title='Valider' style={styles.w100} onPress={() => { setStep5(deviceArray) }} />
        </View>

      </View>
    </View>
  )
}

export default Step5CustomConsumption
