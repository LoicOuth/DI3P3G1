import { useContext } from 'react'
import { type AuthContextData } from '../core/interfaces/auth.interface'
import { AuthContext } from '../providers/auth.provider'

const useAuth = (): AuthContextData => {
  const context = useContext(AuthContext)

  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider')
  }

  return context
}

export default useAuth
