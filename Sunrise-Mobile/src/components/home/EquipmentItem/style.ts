import colors from '@app/styles/colors'
import { StyleSheet } from 'react-native'
export const equipmentStyle = StyleSheet.create({
  round: {
    backgroundColor: colors.secondary,
    height: 20,
    width: 20,
    borderRadius: 100,
    position: 'absolute',
    bottom: 5,
    right: 5
  }
})
