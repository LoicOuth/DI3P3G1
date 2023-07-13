/* eslint-disable indent */
import React from 'react'
import Step1 from '@app/components/steps/Step1'
import styles from '@app/styles'
import { Text, TouchableOpacity, View } from 'react-native'
import Ionicons from '@expo/vector-icons/Ionicons'
import colors from '@app/styles/colors'
import useStepper from '@app/hooks/useStepper'
import Step2CustomConsumption from '@app/components/steps/CustomConsumption/Step2'
import ProgressBar from '@app/components/general/ProgressBar'
import Step3CustomConsumption from '@app/components/steps/CustomConsumption/Step3'
import Step2EnedisConsumption from '@app/components/steps/EnedisConsumption/Step2'
import Step3EnedisConsumption from '@app/components/steps/EnedisConsumption/Step3'
import Step4CustomConsumption from '@app/components/steps/CustomConsumption/Step4'
import Step5CustomConsumption from '@app/components/steps/CustomConsumption/Step5'
import Step6CustomConsumption from '@app/components/steps/CustomConsumption/Step6'
import Step7CustomConsumption from '@app/components/steps/CustomConsumption/Step7'
import { stepperStyles } from './style'

const Stepper = (): React.ReactElement => {
  const { step, back, data } = useStepper()

  const ShowStep = (): React.ReactElement => {
    switch (step) {
      case 1:
        return <Step1 />

      case 2:
        return data.isCustomConsumption ? <Step2CustomConsumption /> : <Step2EnedisConsumption/>
      case 3:
        return data.isCustomConsumption ? <Step3CustomConsumption /> : <Step3EnedisConsumption/>

      case 4:
        return <Step4CustomConsumption />

      case 5:
        return <Step5CustomConsumption />

      case 6:
        return <Step6CustomConsumption />

      case 7:
        return <Step7CustomConsumption />

      default:
        return <Step1 />
    }
  }

  return (
      <View style={styles.flex1}>
        {step !== 1 &&
        <View style={[styles.justifyCenter, styles.flexColumn, styles.w100, styles.flex1]}>
          <View style={[styles.flexRow, styles.alignCenter, styles.flex2]}>
            <TouchableOpacity onPress={() => { back((step === 4 && !data.startDate ? 2 : 1)) }}>
              <Ionicons name='chevron-back-outline' size={70} color={colors.primary} />
            </TouchableOpacity>
            <View style={styles.w70}>
              <Text style={[styles.textLg, styles.textCenter]}>
                {data.isCustomConsumption ? 'Ajouter vos habitudes de consommation' : 'Utiliser vos données de consommation réel'}
                </Text>
            </View>
          </View>
          <View style={stepperStyles.progressBarContainer}>
            <ProgressBar curr={step} max={data.isCustomConsumption ? 7 : 3} />
          </View>
        </View>
        }
        <View style={{ flex: 4 }}>
          <ShowStep />
        </View>
    </View>
  )
}

export default Stepper
