/* eslint-disable @typescript-eslint/indent */
import { type FormUpdateTwin } from '@app/core/interfaces/forms.interface'
import { getGeniusModeState } from '@app/services/geniusMode.service'
import { updateChauffe } from '@app/services/iot.service'
import styles from '@app/styles'
import colors from '@app/styles/colors'
import React from 'react'
import { View, Text, Switch, ActivityIndicator } from 'react-native'
import { useMutation, useQuery, useQueryClient } from 'react-query'
import { equipmentStyle } from './style'
import Icon from 'react-native-vector-icons/MaterialCommunityIcons'

interface props {
  icon: React.ReactElement
  text: string
  status?: boolean
}

const EquipmentItem = ({ icon, text, status }: props): React.ReactElement => {
  const queryClient = useQueryClient()

  const handleUpdateChauffeEau = useMutation(async (form: FormUpdateTwin) => { await updateChauffe(form) }, {
    onSuccess: async () => {
      await queryClient.invalidateQueries({ queryKey: 'currentChauffe' })
    }
  })

  const { data } = useQuery('getGeniusModeState', getGeniusModeState)

  return (
    <View style={[styles.justifyCenter, styles.alignCenter, styles.mt10]}>
      <View style={{ borderRadius: 100, backgroundColor: colors.primary, padding: 15 }}>
        {icon}
        {status && <View style={ equipmentStyle.round }></View>}
      </View>
      <Text>{text}</Text>
      {
        handleUpdateChauffeEau.isLoading
          ? <ActivityIndicator style={styles.mt20} color={colors.primary}/>
          : !data
            ? <Switch
              trackColor={{ false: '#767577', true: colors.primary }}
              thumbColor={status ? colors.secondary : '#4B4B4B'}
              value={status}
              style={styles.mt20}
              onValueChange={(isChauffeEauEnabled) => {
                handleUpdateChauffeEau.mutate({
                  isChauffeEauEnabled
                })
              } }
            />
            : <View style={[styles.alignEnd, styles.flexRow]}>
              <Icon name='leaf' size={20} color={colors.secondary} />
              <Text style={[styles.mt20, styles.ml10]}>Mode genius activé</Text>
            </View>
      }
    </View>
  )
}

export default EquipmentItem
