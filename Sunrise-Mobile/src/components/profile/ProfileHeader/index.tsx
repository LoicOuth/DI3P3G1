import React from 'react'
import { Image, Text, View } from 'react-native'
import { LinearGradient } from 'expo-linear-gradient'
import useAuth from '@app/hooks/useAuth'
import { profileHeaderStyle } from '@app/components/profile/ProfileHeader/style'

const LOGO = require('../../../../assets/user_logo_1.png')

const ProfileHeader = (): React.ReactElement => {
  const { authData } = useAuth()

  return (
    <View style={profileHeaderStyle.container}>
      <LinearGradient colors={['#00ADEF', '#1B61B6']} style={profileHeaderStyle.linearGradient} />
      <View style={profileHeaderStyle.logoContainer}>
        <Image source={LOGO} style={profileHeaderStyle.logo} />
      </View>
      <View style={profileHeaderStyle.detailsContainer}>
        <Text style={profileHeaderStyle.nameText}>{authData?.user?.firstName} {authData?.user?.lastName}</Text>
        <Text style={profileHeaderStyle.refText}>Référence : {authData?.user?.id}</Text>
      </View>
    </View>
  )
}

export default ProfileHeader
