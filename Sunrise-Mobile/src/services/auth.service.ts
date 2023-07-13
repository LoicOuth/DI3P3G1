import { type AuthResponse } from '@app/core/interfaces/auth.interface'
import { type FormAuth } from '@app/core/interfaces/forms.interface'
import { type AuthUser } from '@app/core/interfaces/user.interface'
import myFetch from '@app/core/utils/myFetching'

export const login = async (formData: FormAuth): Promise<AuthResponse> => {
  return await myFetch.post<AuthResponse>('basicauth/login', formData).json()
}

export const loginTest = async (): Promise<AuthUser> => {
  return await myFetch.get<AuthUser>('basicauth/me').json()
}
