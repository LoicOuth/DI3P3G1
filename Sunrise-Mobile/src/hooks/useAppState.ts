import { useEffect } from 'react'
import { AppState, type AppStateStatus, Platform } from 'react-native'
import { focusManager } from 'react-query'

const useAppState = (): void => {
  const onAppStateChange = (status: AppStateStatus): void => {
    // React Query already supports in web browser refetch on window focus by default
    if (Platform.OS !== 'web') {
      focusManager.setFocused(status === 'active')
    }
  }

  useEffect(() => {
    const appState = AppState.addEventListener('change', onAppStateChange)
    return () => { appState.remove() }
  }, [onAppStateChange])
}

export default useAppState
