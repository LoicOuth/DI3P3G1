import { type CurrentData, type GraphData } from './../core/interfaces/consuption.interface'
import myFetch from '@app/core/utils/myFetching'

export const getProductionYersteday = async (): Promise<GraphData[]> => {
  return await myFetch.get<GraphData[]>('production/yesterday').json()
}

export const getAutonom = async (): Promise<CurrentData> => {
  return await myFetch.get<CurrentData>('production/current').json()
}
