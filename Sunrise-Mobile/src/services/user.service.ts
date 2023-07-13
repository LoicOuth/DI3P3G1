import myFetch from '@app/core/utils/myFetching'
import { type AuthUser } from '@app/core/interfaces/user.interface'

export const updateUserDeviceId = async (deviceId: string): Promise<AuthUser> => {
  return await myFetch.put<AuthUser>('user/device', deviceId).json()
}
