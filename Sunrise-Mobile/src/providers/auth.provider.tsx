import React, { useEffect } from 'react'
import AsyncStorage from '@react-native-async-storage/async-storage'
import { createContext, type ReactNode, useState } from 'react'
import { type AuthResponse, type AuthContextData, type AuthData } from '../core/interfaces/auth.interface'
import { type AuthUser } from '@app/core/interfaces/user.interface'

export const AuthContext = createContext<AuthContextData>({} as any)

interface Props {
  children: ReactNode
}

export const AuthProvider: React.FC<Props> = ({ children }) => {
  const [authData, setAuthData] = useState<AuthData>()
  const [isLoading, setIsLoading] = useState<boolean>(true)

  useEffect(() => {
    void loadStorageAuthData()
  }, [])

  const loadStorageAuthData = async (): Promise<void> => {
    try {
      const authDataSerialized = await AsyncStorage.getItem('@AuthData')

      if (authDataSerialized) {
        const authData: AuthResponse = JSON.parse(authDataSerialized)
        setAuthData({
          isLogin: true,
          ...authData
        })
      }
    } catch (e) {
      console.log(e)
    } finally {
      setIsLoading(false)
    }
  }

  const updateUser = (authUser: AuthUser): void => {
    setAuthData({
      isLogin: true,
      token: authData?.token,
      user: authUser
    })

    void AsyncStorage.setItem('@AuthData', JSON.stringify({
      token: authData?.token,
      user: authUser
    }))
  }

  const signIn = (authResponse: AuthResponse): void => {
    setAuthData({
      isLogin: true,
      ...authResponse
    })

    void AsyncStorage.setItem('@AuthData', JSON.stringify(authResponse))
  }

  const signOut = async (): Promise<void> => {
    setAuthData({
      isLogin: false,
      user: undefined,
      token: undefined
    })

    await AsyncStorage.removeItem('@AuthData')
  }

  return (
    // eslint-disable-next-line @typescript-eslint/no-misused-promises
    <AuthContext.Provider value={{ authData, isLoading, signIn, signOut, updateUser }}>
      {children}
    </AuthContext.Provider>
  )
}
