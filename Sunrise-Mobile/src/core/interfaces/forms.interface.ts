export interface FormAuth {
  email: string
  password: string
}

export interface FormDate {
  startDate: string
  endDate: string
}

export interface ConsumptionStepperContext {
  data: ConsumptionStepper
  step: number
  back: (number: number) => void
  setStep1: (isConsumptionCustom: boolean) => void
  setStep2: (isRemote: boolean) => void
  setStep3: (form: FormDate) => void
  setStep4: (numberPeople: number) => void
  setStep5: (devicesId: string[]) => void
  setStep6: (nbMachine: number) => void
  setStep7: (estimation: number) => void
  setEnedisStep3: () => void
}

export interface ConsumptionStepper {
  isCustomConsumption: boolean
  isRemote?: boolean
  startDate?: string
  endDate?: string
  peopleNumber?: number
  device?: string[]
  washingNumber?: number
}

export interface FormEnedisSetting {
  token: string
  pdm: string
}

export interface FormUpdateTwin {
  isChauffeEauEnabled: boolean
}

export interface OcrParams {
  ocrText: string
}
