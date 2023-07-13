import { type ConsumptionStepperContext } from '@app/core/interfaces/forms.interface'
import { StepperContext } from '@app/providers/stepper.provider'
import { useContext } from 'react'

const useStepper = (): ConsumptionStepperContext => {
  const context = useContext(StepperContext)

  if (!context) {
    throw new Error('useAuth must be used within an AuthProvider')
  }

  return context
}

export default useStepper
