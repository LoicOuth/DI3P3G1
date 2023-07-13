import { type IDevice } from './device.interface'

export interface Consumption {
  startDate: Date
  endDate: Date
  peopleNumber: number
  device: IDevice[]
  washingNumber: number
  estimatedConsumption: number
}

export interface GraphData {
  time: string
  value: number
}

export interface CurrentData {
  productionInstantane: number
  consommationInstantane: number
  tauxAutonomie: number
}
