import { StyleSheet } from 'react-native'

export const profileStyle = StyleSheet.create({
  headerContainer: {
    backgroundColor: 'transparent',
    borderBottomLeftRadius: 20,
    borderBottomRightRadius: 20,
    overflow: 'hidden'
  },
  screenContainer: {
    flex: 1,
    alignItems: 'center'
  },
  screenText: {
    fontSize: 24,
    fontWeight: 'bold',
    alignSelf: 'flex-start'
  },
  separator: {
    width: '100%',
    height: 1,
    backgroundColor: '#ccc'
  },
  imageContainer: {
    width: '100%',
    height: '100%'
  },
  bottomContainer: {
    marginBottom: 10
  },
  modalContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: 'rgba(0, 0, 0, 0.5)'
  },
  modalBox: {
    width: '80%',
    backgroundColor: '#fff',
    borderRadius: 10,
    padding: 20,
    alignItems: 'center'
  },
  modalText: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 20
  },
  modalButtonsContainer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    width: '100%'
  },
  modalButtonYes: {
    backgroundColor: '#00ADEF',
    paddingHorizontal: 30,
    paddingVertical: 10,
    borderRadius: 5
  },
  modalButtonNo: {
    backgroundColor: '#ED1B24',
    paddingHorizontal: 30,
    paddingVertical: 10,
    borderRadius: 5
  },
  modalButtonText: {
    color: '#fff',
    fontWeight: 'bold'
  },
  test: {
    alignItems: 'center',
    justifyContent: 'center',
    flex: 1,
    width: '100%',
    height: '100%'
  }
})
