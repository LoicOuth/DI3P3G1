import PrimaryButton from '@app/components/general/PrimaryButton'
import { type FormDate } from '@app/core/interfaces/forms.interface'
import useStepper from '@app/hooks/useStepper'
import styles from '@app/styles'
import React from 'react'
import { Controller, useForm } from 'react-hook-form'
import { Text, View } from 'react-native'
import MaskInput from 'react-native-mask-input'

const Step3CustomConsumption = (): React.ReactElement => {
  const { setStep3, data } = useStepper()

  const { control, handleSubmit, setError, formState: { errors } } = useForm<FormDate>({
    defaultValues: {
      startDate: data.startDate ? data.startDate : '08:00',
      endDate: data.endDate ? data.endDate : '17:00'
    }
  })

  const onValidate = (data: FormDate): void => {
    const regex = /^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$/

    if (regex.test(data.startDate)) {
      if (regex.test(data.endDate)) {
        if (parseInt(data.startDate) < parseInt(data.endDate)) {
          setStep3(data)
        } else {
          setError('startDate', { type: 'custom', message: 'Date de début supérieur à la date de fin !' })
        }
      } else {
        setError('startDate', { type: 'custom', message: 'Erreur de saisie !' })
      }
    } else { setError('startDate', { type: 'custom', message: 'Erreur de saisie !' }) }
  }

  return (
    <View style={[styles.flex1, styles.alignCenter]}>
      <Text style={[styles.textXl, styles.textGray, styles.textCenter]}>Ajoutez vos horraires de travail</Text>
      <View style={[styles.flexColumn, styles.alignCenter, styles.justifyEvenly, styles.w100, styles.flex1, { paddingHorizontal: 30 }]}>
        {(errors.startDate && errors.startDate.type === 'custom') && <Text style={[styles.textError, styles.ml10]}>{errors.startDate.message}</Text>}
        <View style={[styles.w100]}>
          <Controller
            control={control}
            rules={{
              required: true
            }}
            render={({ field: { onChange, onBlur, value } }) => (
              <MaskInput
                style={[styles.bigInput, styles.w100]}
                keyboardType='number-pad'
                mask={[/\d/, /\d/, ':', /\d/, /\d/]}
                onChangeText={onChange}
                onBlur={onBlur}
                value={value}
              />
            )}
            name='startDate'
          />
          {(errors.startDate && errors.startDate.type !== 'custom') && <Text style={[styles.textError, styles.ml10]}>Ce champs est obligatoire</Text>}
        </View>

        <View style={[styles.w100]}>
          <Controller
            control={control}
            rules={{
              required: true
            }}
            render={({ field: { onChange, onBlur, value } }) => (
              <MaskInput
                style={[styles.bigInput, styles.w100]}
                keyboardType='numeric'
                mask={[/\d/, /\d/, ':', /\d/, /\d/]}
                onChangeText={onChange}
                onBlur={onBlur}
                value={value}
              />
            )}
            name='endDate'
          />
          {(errors.startDate && errors.startDate.type !== 'custom') && <Text style={[styles.textError, styles.ml10]}>Ce champs est obligatoire</Text>}
        </View>

        <PrimaryButton title='Valider' style={styles.w100} onPress={
          handleSubmit((data: FormDate) => { onValidate(data) })
        } />
      </View>
    </View>

  )
}

export default Step3CustomConsumption
