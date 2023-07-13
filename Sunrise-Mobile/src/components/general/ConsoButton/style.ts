import { StyleSheet } from 'react-native'

const consoButtonStyles = StyleSheet.create({
  buttonContainer: {
    width: '100%',
    height: 80,
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 20
  },
  buttonText: {
    flex: 1,
    marginLeft: 10
  },
  iconContainer: {
    width: 50,
    height: 50,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#FFFFFF',
    borderRadius: 25,
    marginRight: 10
  },
  whiteBackground: {
    backgroundColor: '#FFFFFF'
  },
  arrowContainer: {
    width: 30,
    height: 30,
    justifyContent: 'center',
    alignItems: 'center',
    marginLeft: 10
  }
})

export default consoButtonStyles
