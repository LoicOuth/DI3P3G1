import { type AuthUser } from './user.interface'

export interface AuthContextData {
  authData?: AuthData
  isLoading: boolean
  signIn: (authResponse: AuthResponse) => void
  signOut: () => void
  updateUser: (authUser: AuthUser) => void
}

export interface AuthData {
  isLogin: boolean
  user?: AuthUser
  token?: string
}

export interface AuthResponse {
  user: AuthUser
  token: string
}
