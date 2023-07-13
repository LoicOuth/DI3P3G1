/* eslint-disable @typescript-eslint/no-invalid-void-type */
/* eslint-disable @typescript-eslint/no-confusing-void-expression */
import myFetch from '@app/core/utils/myFetching'

export const getGeniusModeState = async (): Promise<boolean> => {
  return await myFetch.get<boolean>('geniusmode/state').json()
}

export const changeState = async (): Promise<void> => {
  return await myFetch.put<void>('geniusmode/updatestate', null).json()
}
