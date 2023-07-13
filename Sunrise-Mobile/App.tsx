import React from 'react'
import { StatusBar } from 'react-native'
import { QueryClientProvider, QueryClient } from 'react-query'
import useAppState from './src/hooks/useAppState'
import { useOnlineManager } from './src/hooks/useOnlineManager'
import Router from './src/navigation/main'
import { AuthProvider } from './src/providers/auth.provider'

const queryClient = new QueryClient()

const App = (): React.ReactElement => {
  useOnlineManager()
  useAppState()
  StatusBar.setHidden(true)

  return (
    <QueryClientProvider client={queryClient}>
      <AuthProvider>
        <Router />
      </AuthProvider>
    </QueryClientProvider>
  )
}

export default App
