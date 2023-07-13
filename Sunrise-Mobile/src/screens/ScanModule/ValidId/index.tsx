import { type OcrParams } from '@app/core/interfaces/forms.interface'
import styles from '@app/styles'
import { type RouteProp, useRoute, useNavigation } from '@react-navigation/native'
import React, { useState, type ReactElement } from 'react'
import { View, Text, TouchableOpacity, TextInput } from 'react-native'
import Ionicons from '@expo/vector-icons/Ionicons'
import colors from '@app/styles/colors'
import PrimaryButton from '@app/components/general/PrimaryButton'
import { useMutation } from 'react-query'
import { updateUserDeviceId } from '@app/services/user.service'
import useAuth from '@app/hooks/useAuth'

const ValidId = (): ReactElement => {
  const { updateUser } = useAuth()
  const { params } = useRoute<RouteProp<{ params: OcrParams }, 'params'>>()
  const nav = useNavigation()
  const [deviceId, setDeviceId] = useState(params.ocrText)

  const handleupdate = useMutation(async (data: string) => await updateUserDeviceId(data), {
    onSuccess: (data) => {
      updateUser(data)
    }
  })

  return (
    <View style={[styles.flex1, styles.alignCenter, { backgroundColor: 'white' }]}>
      <View style={[styles.flex1, styles.w100, styles.flexRow, styles.alignCenter]}>
        <TouchableOpacity onPress={() => { nav.goBack() }}>
          <Ionicons name='chevron-back-outline' size={70} color={colors.primary} />
        </TouchableOpacity>
        <Text style={[styles.textLg, styles.textBold, styles.textCenter]}>Valider l&apos;identifiant du module</Text>
      </View>
      <View style={[styles.flex4, styles.w80, styles.justifyCenter]}>
        <TextInput
          style={styles.textArea}
          multiline={true}
          numberOfLines={4}
          value={deviceId}
          onChangeText={(value) => { setDeviceId(value) }}
        />
      </View>
      <View style={[styles.flex1, styles.w100, styles.alignCenter]}>
        <PrimaryButton title='Valider' style={[styles.w80, styles.mb20]} isLoading={handleupdate.isLoading} onPress={() => { handleupdate.mutate(deviceId) }} />
      </View>
    </View>
  )
}

export default ValidId
