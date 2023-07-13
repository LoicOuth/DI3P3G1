import { type GraphData } from './../core/interfaces/consuption.interface'
import { type Consumption } from '@app/core/interfaces/consuption.interface'
import myFetch from '@app/core/utils/myFetching'

export const estimation = async (): Promise<Consumption> => {
  return await myFetch.get<Consumption>('consumption/settings').json()
}

export const getConsumption = async (isPdm: boolean): Promise<GraphData[]> => {
  const url = `${isPdm ? 'enedisconsumption' : 'consumption'}/yesterday`
  return await myFetch.get<GraphData[]>(url).json()
}
