/* eslint-disable @typescript-eslint/no-confusing-void-expression */
import { type FormUpdateTwin } from '@app/core/interfaces/forms.interface'
import myFetch from '@app/core/utils/myFetching'

export const getChauffeEau = async (): Promise<boolean> => {
  return await myFetch.get<boolean>('iot/gettwin').json()
}

export const updateChauffe = async (form: FormUpdateTwin): Promise<void> => {
  // eslint-disable-next-line @typescript-eslint/no-invalid-void-type
  return await myFetch.put<void>('iot/updatetwin', form).json()
}
