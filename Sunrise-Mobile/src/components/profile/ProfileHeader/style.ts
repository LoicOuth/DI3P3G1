import { StatusBar, StyleSheet } from 'react-native'

export const profileHeaderStyle = StyleSheet.create({
  container: {
    flexDirection: 'row',
    alignItems: 'center',
    height: 'auto',
    paddingTop: StatusBar.currentHeight ?? 45,
    paddingHorizontal: 10
  },
  logoContainer: {
    width: 40,
    height: 40,
    borderRadius: 20,
    overflow: 'hidden',
    marginRight: 10,
    alignSelf: 'flex-start'
  },
  logo: {
    flex: 1,
    width: '100%',
    height: '100%',
    resizeMode: 'cover'
  },
  detailsContainer: {
    flex: 1
  },
  nameText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 5
  },
  refText: {
    color: '#fff',
    fontSize: 16,
    paddingBottom: 8
  },
  linearGradient: {
    position: 'absolute',
    left: 0,
    right: 0,
    top: 0,
    bottom: 0,
    borderBottomLeftRadius: 20,
    borderBottomRightRadius: 20
  }
})
