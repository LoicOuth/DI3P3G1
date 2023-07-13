import PrimaryButton from '@app/components/general/PrimaryButton'
import { BtnSize } from '@app/core/enums/btnSize.enum'
import useStepper from '@app/hooks/useStepper'
import styles from '@app/styles'
import React from 'react'
import { Image, View } from 'react-native'

const LOGO = require('../../../../assets/sunrise_logo.png')

const Step1 = (): React.ReactElement => {
  const { setStep1 } = useStepper()

  return (
    <View style={[styles.flex1, styles.flexColumn, styles.alignCenter]}>
      <View style={[styles.flex1, styles.justifyCenter]}>
        <Image source={LOGO} style={{ width: 150, height: 150 }} />
      </View>
      <View style={[styles.w70, styles.flex2]}>
        <PrimaryButton
          title='Utiliser votre consommation réel'
          size={BtnSize.SMALL}
          onPress={() => { setStep1(false) } }
        />
        <PrimaryButton
          title='Entrer vos habitudes de consommation'
          size={BtnSize.SMALL}
          onPress={() => { setStep1(true) }}
          style={styles.mt20}
        />
      </View>

    </View>
  )
}

export default Step1
