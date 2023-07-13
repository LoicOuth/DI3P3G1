import myFetch from '@app/core/utils/myFetching'

export const uploadImage = async (uri: string): Promise<string> => {
  const fileName = uri.split('/').pop()
  const fileType = fileName?.split('.').pop()

  const formData = new FormData()
  formData.append('file', {
    uri,
    name: fileName ?? 'test',
    type: fileType ? `image/${fileType}` : 'image/jpg'
  } as unknown as Blob)

  return await myFetch.post('ocr', formData, true, {
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
    .text()
}
