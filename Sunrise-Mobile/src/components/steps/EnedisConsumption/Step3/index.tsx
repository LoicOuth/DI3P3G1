import React from 'react'
import { Linking, Text, TouchableOpacity, View } from 'react-native'
import styles from '@app/styles'
import { useForm } from 'react-hook-form'
import { type FormEnedisSetting } from '@app/core/interfaces/forms.interface'
import Input from '@app/components/general/Input'
import PrimaryButton from '@app/components/general/PrimaryButton'
import { useMutation } from 'react-query'
import { setEnedisSettings } from '@app/services/stepper.service'
import useAuth from '@app/hooks/useAuth'

const supportedURL = 'https://github.com/DI3P3G1/doc/blob/main/README.md'

const Step3EnedisConsumption = (): React.ReactElement => {
  const { updateUser } = useAuth()
  const { control, handleSubmit, formState: { errors } } = useForm<FormEnedisSetting>()

  const handleSet = useMutation(async (form: FormEnedisSetting) => await setEnedisSettings(form), {
    onSuccess: (data) => { updateUser(data) }
  })

  return (
    <View style={[styles.flex1, styles.alignCenter]}>
      <View style={styles.flex1}>
        <Text style={styles.textLg}>Pour plus dinfo consulter cette page :</Text>
        <TouchableOpacity style={styles.alignCenter} onPress={async () => await Linking.openURL(supportedURL)}>
          <Text style={[styles.textXl, styles.textUnderline, styles.textPrimary]}>Documentation</Text>
        </TouchableOpacity>
      </View>

      <View style={[styles.flex5, styles.w80]}>
        <Input
          name='pdm'
          label='Entrer le PDM/PDL'
          error={errors.pdm}
          control={control}
          style={styles.mt20}
        />

        <Input
          name='token'
          label='Entrer le token'
          error={errors.token}
          control={control}
          style={styles.mt20}
          password={true}
        />

        <PrimaryButton
          title='Finaliser'
          isLoading={handleSet.isLoading}
          style={{ marginTop: 80 }}
          onPress={handleSubmit((data: FormEnedisSetting) => { handleSet.mutate(data) })}
        />
      </View>
    </View>
  )
}

export default Step3EnedisConsumption
