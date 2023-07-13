export interface IDevice {
  id: string
  name: string
  value: number
}

export interface DeviceResponse {
  devices: IDevice[]
}
