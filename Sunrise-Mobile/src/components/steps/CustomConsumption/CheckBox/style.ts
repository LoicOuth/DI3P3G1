import colors from '@app/styles/colors'
import { StyleSheet } from 'react-native'

export const checkboxStyles = StyleSheet.create({
  checkbox: {
    borderWidth: 2,
    borderColor: colors.textGray,
    borderRadius: 15,
    padding: 20,
    marginHorizontal: 10
  },

  checkboxText: {
    fontSize: 20,
    fontWeight: '600'
  },

  checkboxSelected: {
    backgroundColor: colors.primary,
    color: 'white'
  }

})
