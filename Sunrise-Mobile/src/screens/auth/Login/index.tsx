import Input from '@app/components/general/Input'
import PrimaryButton from '@app/components/general/PrimaryButton'
import { type FormAuth } from '@app/core/interfaces/forms.interface'
import useAuth from '@app/hooks/useAuth'
import { login } from '@app/services/auth.service'
import React from 'react'
import { useForm } from 'react-hook-form'
import { Image, View } from 'react-native'
import { useMutation } from 'react-query'
import loginStyle from './style'
import styles from '@app/styles'
import { LinearGradient } from 'expo-linear-gradient'
import useKeyboardVisibility from '@app/hooks/useKeyboardVisibility'

const LOGO = require('../../../../assets/sunrise_logo_white.png')
const IMAGE = require('../../../../assets/login_image.png')

const LoginScreen = (): React.ReactElement => {
  const { signIn } = useAuth()
  const isKeyboardVisible = useKeyboardVisibility()

  const { control, handleSubmit, formState: { errors } } = useForm<FormAuth>()

  const handleLogin = useMutation(async (form: FormAuth) => await login(form), {
    onSuccess: (data) => { signIn(data) }
  })

  return (
    <View style={styles.h100}>
      <LinearGradient colors={['#1B61B6', 'rgba(0, 173, 239, 0.2)']} style={[loginStyle.princpialeView, styles.flex5]}>
        <Image source={LOGO} style={{ width: 60, height: 60, margin: 20 }}/>
        { !isKeyboardVisible && <View style={styles.alignCenter}><Image source={IMAGE} /></View> }
        <Input
          name='email'
          label='Adresse email'
          error={errors.email}
          control={control}
          icon="at-sharp"
          style={styles.mt20}
          keyboardType='email-address'
        />
        <Input
          name='password'
          label='Mot de passe'
          error={errors.password}
          control={control}
          password={true}
          icon="lock-closed-outline"
          style={styles.mt20}
        />
      </LinearGradient>
      <View style={[loginStyle.btnContainer, styles.flex1]}>
        <PrimaryButton
          title='Connexion'
          isLoading={handleLogin.isLoading}
          onPress={handleSubmit((data: FormAuth) => { handleLogin.mutate(data) })}
        />
      </View>
    </View>
  )
}

export default LoginScreen
