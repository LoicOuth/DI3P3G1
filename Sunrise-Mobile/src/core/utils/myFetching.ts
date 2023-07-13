import Constants from 'expo-constants'
import { type AuthResponse } from '../interfaces/auth.interface'
import AsyncStorage from '@react-native-async-storage/async-storage'

interface IMyFetching {
  get: <T>(url: string, config?: RequestInit) => IFetch<T>
  post: <T>(url: string, data: unknown, fileUpload?: boolean, config?: RequestInit) => IFetch<T>
  put: <T>(url: string, data: unknown, config?: RequestInit) => IFetch<T>
}

interface IFetch<T> {
  text: () => Promise<string>
  json: () => Promise<T>
}

const myFetching = (baseUrl: string, interceptor: (request: RequestInit) => Promise<RequestInit>, defaultRequest: RequestInit): IMyFetching => {
  const fetch = <T>(url: string, config: RequestInit): IFetch<T> => {
    let request: RequestInit = {
      ...defaultRequest,
      ...config
    }

    const finalUrl = `${baseUrl}/${url}`

    return {
      text: async () => {
        if (interceptor) {
          request = await interceptor(request)
        }

        const response = await window.fetch(finalUrl, request)

        if (!response.ok) {
          return await Promise.reject(await response.text())
        }

        return await response.text()
      },

      json: async () => {
        if (interceptor) {
          request = await interceptor(request)
        }

        const response = await window.fetch(finalUrl, request)

        if (!response.ok) {
          return await Promise.reject(await response.json())
        }

        try {
          return await response.json()
        } catch (error) {
          return null as any as T
        }
      }
    }
  }

  return {
    get: <T>(url: string, config?: RequestInit): IFetch<T> => {
      return fetch(url, { ...config, method: 'GET' })
    },

    post: <T>(url: string, data: unknown, fileUpload: boolean = false, config?: RequestInit): IFetch<T> => {
      return fetch(url, { ...config, method: 'POST', body: fileUpload ? data as FormData : JSON.stringify(data) })
    },

    put: <T>(url: string, data: unknown, config?: RequestInit): IFetch<T> => {
      return fetch(url, { ...config, method: 'PUT', body: JSON.stringify(data) })
    }
  }
}

const interceptor = async (request: RequestInit): Promise<RequestInit> => {
  const authDataSerialized = await AsyncStorage.getItem('@AuthData')

  if (authDataSerialized) {
    const authData: AuthResponse = JSON.parse(authDataSerialized)
    if (request.headers) {
      // eslint-disable-next-line @typescript-eslint/dot-notation
      request.headers['Authorization'] = `Basic ${authData.token}`
    }
  }

  return request
}

const defaultRequestInit: RequestInit = {
  headers: {
    'Content-Type': 'application/json',
    Accept: 'application/json'
  }
}

const myFetch = myFetching(
  Constants.expoConfig?.extra?.apiUrl,
  interceptor,
  defaultRequestInit
)

export default myFetch
