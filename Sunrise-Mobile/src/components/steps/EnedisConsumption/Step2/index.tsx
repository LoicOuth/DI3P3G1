import React from 'react'
import useStepper from '@app/hooks/useStepper'
import { Image, Text, View } from 'react-native'
import styles from '@app/styles'
import PrimaryButton from '@app/components/general/PrimaryButton'
const LINKY = require('../../../../../assets/linky.jpg')

const Step2EnedisConsumption = (): React.ReactElement => {
  const { setEnedisStep3 } = useStepper()

  return (
    <View style={[styles.flex1, styles.alignCenter]}>
      <View style={[styles.flex1, styles.alignCenter, styles.justifyCenter]}>
        <Text style={[styles.textError, styles.textXl, styles.textCenter]}>Attention ! Vous devez posséder un compteur communicant pour pouvoir continuer</Text>
        <Image style={{ height: 200, resizeMode: 'contain', marginTop: 20 }} source={LINKY}></Image>
      </View>
      <View style={[styles.alignCenter, styles.justifyCenter, styles.flex1, styles.w100]}>
        <View style={[styles.flexColumn, styles.alignCenter, styles.justifyCenter, styles.w80, {
          flex: 0.3
        }]}>
          <PrimaryButton title="Valider" style={[styles.w100]} onPress={() => { setEnedisStep3() }}/>
        </View>
      </View>
    </View>
  )
}

export default Step2EnedisConsumption
