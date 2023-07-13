import colors from '@app/styles/colors'
import { StyleSheet } from 'react-native'

export const progressStyles = StyleSheet.create({
  progressContainer: {
    width: '100%',
    height: 15,
    backgroundColor: '#D9D9D9',
    borderRadius: 50
  },

  progress: {
    backgroundColor: colors.primary,
    position: 'absolute',
    height: '100%',
    borderRadius: 50,
    left: 0
  }
})
