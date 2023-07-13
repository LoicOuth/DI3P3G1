import React, { useState } from 'react'
import {
  View,
  Text,
  Image,
  Modal,
  TouchableOpacity
} from 'react-native'
import ConsoButton from '@app/components/general/ConsoButton'
import useAuth from '@app/hooks/useAuth'
import { useNavigation } from '@react-navigation/native'
import { profileStyle } from '@app/screens/Profile/style'
import ProfileHeader from '@app/components/profile/ProfileHeader'
import styles from '@app/styles'
import { useMutation, useQuery, useQueryClient } from 'react-query'
import { changeState, getGeniusModeState } from '@app/services/geniusMode.service'

const BACK_IMG = require('../../../assets/fond_profile.png')

const ProfileScreen = (): React.ReactElement => {
  const { signOut } = useAuth()
  const queryClient = useQueryClient()

  const [showModal, setShowModal] = useState(false)
  const navigation = useNavigation()

  const { data, isLoading } = useQuery('getGeniusModeState', getGeniusModeState)
  const handleStateChange = useMutation(async () => { await changeState() }, {
    onSuccess: async () => { await queryClient.invalidateQueries({ queryKey: 'getGeniusModeState' }) }
  })

  return (
    <View style={profileStyle.screenContainer}>
      <ProfileHeader />
      <Text style={[profileStyle.screenText, styles.mt10]}>Ma consommation</Text>
      <View style={[styles.mt10]}>
        <ConsoButton title="Visualiser mes habitudes de consommation" icon="lightbulb-variant-outline" color="#A6D81F" onPress={() => { navigation.navigate('ConsumerHabit' as never) }}/>
      </View>
      <View style={profileStyle.separator} />
      <View>
        <ConsoButton title="Modifier mes habitudes de consommation" icon="pencil-outline" color="#1B61B6" onPress={ () => { navigation.navigate('Step' as never) }}/>
      </View>
      <View style={profileStyle.separator} />
      <View>
        {!isLoading && <ConsoButton
          title={`${data ? 'Désactiver' : 'Activer'} le mode genius`}
          icon={data ? 'leaf-off' : 'leaf'}
          endIcon=''
          color="#FBBD0A"
          onPress={ () => { handleStateChange.mutate() }}
          isLoading={handleStateChange.isLoading}
        />
        }
      </View>
      <View style={profileStyle.test}>
        <Image source={BACK_IMG} style={profileStyle.imageContainer}/>
      </View>
      <View style={profileStyle.bottomContainer}>
        <ConsoButton title="Se déconnecter" icon="logout" color="#ED1B24" onPress={() => { setShowModal(true) }}/>
      </View>

      <Modal transparent visible={showModal} onRequestClose={() => { setShowModal(false) }}>
        <View style={profileStyle.modalContainer}>
          <View style={profileStyle.modalBox}>
            <Text style={profileStyle.modalText}>Êtes-vous sûr de vouloir vous déconnecter ?</Text>
            <View style={profileStyle.modalButtonsContainer}>
              <TouchableOpacity onPress={signOut} style={profileStyle.modalButtonYes}>
                <Text style={profileStyle.modalButtonText}>Oui</Text>
              </TouchableOpacity>
              <TouchableOpacity onPress={() => { setShowModal(false) }} style={profileStyle.modalButtonNo}>
                <Text style={profileStyle.modalButtonText}>Non</Text>
              </TouchableOpacity>
            </View>
          </View>
        </View>
      </Modal>
    </View>
  )
}

export default ProfileScreen
