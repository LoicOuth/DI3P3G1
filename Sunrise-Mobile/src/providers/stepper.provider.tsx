import React from 'react'
import { type ConsumptionStepperContext, type ConsumptionStepper, type FormDate } from '@app/core/interfaces/forms.interface'
import { createContext, useState, type ReactNode } from 'react'
import { useMutation } from 'react-query'
import { setEstimation, setSettings } from '@app/services/stepper.service'
import useAuth from '@app/hooks/useAuth'
import { useNavigation } from '@react-navigation/native'

export const StepperContext = createContext<ConsumptionStepperContext>({} as any)

interface Props {
  children: ReactNode
  isNavigate?: boolean
}

export const StepperProvider: React.FC<Props> = ({ children, isNavigate = false }) => {
  const { updateUser } = useAuth()
  const [step, setStep] = useState(1)
  const [data, setData] = useState<ConsumptionStepper>({
    device: undefined,
    isCustomConsumption: false,
    peopleNumber: undefined,
    washingNumber: undefined,
    endDate: undefined,
    isRemote: undefined,
    startDate: undefined
  })

  const navigation = isNavigate ? useNavigation() : undefined

  const back = (number: number): void => {
    setStep(step - number)
  }

  const setStep1 = (isConsumptionCustom: boolean): void => {
    setData({
      isCustomConsumption: isConsumptionCustom
    })
    setStep(step + 1)
  }

  const setStep2 = (isRemote: boolean): void => {
    const lastData = { ...data }
    lastData.isRemote = isRemote
    setData(lastData)

    setStep(isRemote ? step + 2 : step + 1)
  }

  const setStep3 = (form: FormDate): void => {
    const lastData = { ...data }
    lastData.startDate = form.startDate
    lastData.endDate = form.endDate
    setData(lastData)

    setStep(step + 1)
  }
  const setEnedisStep3 = (): void => {
    setStep(step + 1)
  }

  const setStep4 = (numberPeople: number): void => {
    const lastData = { ...data }
    lastData.peopleNumber = numberPeople
    setData(lastData)

    setStep(step + 1)
  }

  const setStep5 = (devicesId: string[]): void => {
    const lastData = { ...data }
    lastData.device = devicesId
    setData(lastData)

    setStep(step + 1)
  }

  const setStep6 = (nbMachine: number): void => {
    const lastData = { ...data }
    lastData.washingNumber = nbMachine
    setData(lastData)

    handleSet.mutate()
  }

  const setStep7 = (estimation: number): void => {
    handleSetEstimation.mutate(estimation)
  }

  const handleSet = useMutation(async () => { await setSettings(data) }, {
    onSuccess: () => {
      setStep(step + 1)
    }
  })

  const handleSetEstimation = useMutation(async (estimation: number) => await setEstimation(estimation), {
    onSuccess: (authUser) => {
      updateUser(authUser)
      if (isNavigate) {
        navigation?.navigate('Profile' as never)
      }
    }
  })

  return (
  // eslint-disable-next-line @typescript-eslint/no-misused-promises
    <StepperContext.Provider value={{ data, step, back, setStep1, setStep2, setStep3, setStep4, setStep5, setStep6, setStep7, setEnedisStep3 }}>
      {children}
    </StepperContext.Provider>
  )
}
