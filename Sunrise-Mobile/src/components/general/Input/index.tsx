import styles from '@app/styles'
import React from 'react'
import { Controller, type FieldError, type Control } from 'react-hook-form'
import { type StyleProp, Text, TextInput, View, type KeyboardTypeOptions } from 'react-native'
import Ionicons from '@expo/vector-icons/Ionicons'

interface props {
  label: string
  name: string
  control: Control<any, any>
  error?: FieldError
  icon?: any
  password?: boolean
  color?: string
  style?: StyleProp<any>
  placeholder?: string
  keyboardType?: KeyboardTypeOptions
}

const Input = ({ label, control, error, name, icon, style, placeholder = `Entrer un ${label}...`, password = false, color = 'white', keyboardType = 'default' }: props): React.ReactElement => {
  return (
    <View style={style}>
      <View style={[styles.flexRow, styles.alignCenter, styles.ml20]}>
        {icon && <Ionicons name={icon} size={30} color={color}/>}
        <Text style={[styles.textLg, styles.ml10, { color }]}>{label}</Text>
      </View>
      <Controller
        control={control}
        rules={{
          required: true
        }}
        render={({ field: { onChange, onBlur, value } }) => (
          <TextInput
            secureTextEntry={password}
            style={styles.bigInput}
            onBlur={onBlur}
            onChangeText={onChange}
            value={value}
            placeholder={placeholder}
            keyboardType={keyboardType}
          />
        )}
        name={name}
      />
      {error && <Text style={[styles.textError, styles.ml10]}>Ce champs est obligatoire</Text>}
    </View>

  )
}

export default Input
