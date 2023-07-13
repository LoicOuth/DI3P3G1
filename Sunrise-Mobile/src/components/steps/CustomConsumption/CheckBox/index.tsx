import useStepper from '@app/hooks/useStepper'
import colors from '@app/styles/colors'
import React from 'react'
import { Text, TouchableOpacity } from 'react-native'
import { checkboxStyles } from './style'

interface props {
  num: number
  onUpdate: (num: number) => void
}

const CheckBox = ({ num, onUpdate }: props): React.ReactElement => {
  const { data } = useStepper()

  return (
    <TouchableOpacity
      onPress={() => { onUpdate(num) }}
      style={[checkboxStyles.checkbox, { backgroundColor: data.peopleNumber === num ? colors.primary : 'white' }]}>
      <Text style={[checkboxStyles.checkboxText, { color: data.peopleNumber === num ? 'white' : colors.textGray }]}>{num}</Text>
    </TouchableOpacity>
  )
}

export default CheckBox
