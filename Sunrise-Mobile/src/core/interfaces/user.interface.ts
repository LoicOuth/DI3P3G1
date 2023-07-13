import { type IDevice } from './device.interface'
export interface AuthUser {
  id: string
  firstName: string
  lastName: string
  email: string
  pdm?: string
  consumption?: Consumption
  deviceId?: string
}

export interface Consumption {
  startDate: string
  endDate: string
  peopleNumber: number
  device: IDevice[]
  washingNumber: number
  estimatedConsumption: number
}
