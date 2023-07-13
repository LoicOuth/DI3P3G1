import { type IMeteo } from '@app/core/interfaces/meteo.interface'
import myFetch from '@app/core/utils/myFetching'

export const getMeteo = async (): Promise<IMeteo> => {
  return await myFetch.get<IMeteo>('meteo').json()
}
