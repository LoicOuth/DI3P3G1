import React from 'react'
import { ActivityIndicator, Text, TouchableOpacity, View } from 'react-native'
import Icon from 'react-native-vector-icons/MaterialCommunityIcons'
import styles from '@app/styles'
import consoButtonStyles from '@app/components/general/ConsoButton/style'
import colors from '@app/styles/colors'

interface props {
  title: string
  icon: string
  color: string
  onPress: () => void
  endIcon?: string
  isLoading?: boolean
}

const ConsoButton = ({ title, icon, color, isLoading = false, endIcon = 'chevron-right', onPress }: props): React.ReactElement => {
  return (
    <TouchableOpacity style={[consoButtonStyles.buttonContainer, styles.btnPrimary, consoButtonStyles.whiteBackground]} onPress={onPress}>
      <View style={consoButtonStyles.iconContainer}>
        <Icon name={icon} size={50} color={color} />
      </View>
      <Text style={[styles.textLg, styles.textBlack, consoButtonStyles.buttonText]}>{title}</Text>
      <View style={consoButtonStyles.arrowContainer}>
        {endIcon.length > 0 && <Icon name={endIcon} size={30} color="#4B4B4B" />}
        {isLoading && <ActivityIndicator size='small' color={colors.primary} /> }
      </View>
    </TouchableOpacity>
  )
}

export default ConsoButton
