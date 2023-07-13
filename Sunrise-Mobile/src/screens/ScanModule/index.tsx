import React, { useState } from 'react'
import PrimaryButton from '@app/components/general/PrimaryButton'
import styles from '@app/styles'
import { Camera } from 'expo-camera'
import { type CameraCapturedPicture, CameraType } from 'expo-camera/build/Camera.types'
import { type ReactElement } from 'react'
import { Image, Text, unstable_batchedUpdates, View } from 'react-native'
import scanStyle from './style'
import { useMutation } from 'react-query'
import { uploadImage } from '@app/services/scan.service'
import { useNavigation } from '@react-navigation/native'

const ScanModule = (): ReactElement => {
  const [permission, requestPermission] = Camera.useCameraPermissions()
  const [showCamera, setShowCamera] = useState(false)
  const [camera, setCamera] = useState<Camera | null>()
  const [loading, setLoading] = useState(false)
  const [photo, setPhoto] = useState<CameraCapturedPicture>()
  const navigation = useNavigation()

  const handleUpload = useMutation(async (uri: string) => await uploadImage(uri), {
    onSuccess: (data) => {
      // eslint-disable-next-line @typescript-eslint/consistent-type-assertions
      navigation.navigate('ValidId' as never, { ocrText: data } as never)
    }
  })

  const checkPerm = (): void => {
    if (permission && !permission.granted) {
      void requestPermission()
    } else {
      unstable_batchedUpdates(() => {
        setPhoto(undefined)
        setShowCamera(true)
      })
    }
  }

  const takePicture = async (): Promise<void> => {
    setLoading(true)
    if (camera) {
      void camera.takePictureAsync().then(data => {
        unstable_batchedUpdates(() => {
          setPhoto(data)
          setLoading(false)
          setShowCamera(false)
        })
      })
    }
  }

  return (
    <View style={[styles.flex1, { backgroundColor: 'white' }]}>
      {showCamera
        ? <Camera style={styles.flex1} type={CameraType.back} ref={(ref) => { setCamera(ref) }}>
          <View style={[styles.absolute, scanStyle.buttonPos, styles.w100, styles.alignCenter]}>
            <PrimaryButton title='Valider' style={styles.w80} isLoading={loading} onPress={async () => { await takePicture() }}/>
          </View>
        </Camera>
        : <View style={styles.flex1}>
          <View style={[styles.flex1, styles.alignCenter, styles.justifyCenter]}>
            <Text style={[styles.textXl, styles.textBold]}>Ajouter votre module</Text>
          </View>
          <View style={[styles.flex5, styles.alignCenter]}>
            {photo
              ? <View style={styles.flex1}><Image source={{ uri: photo.uri }} style={{ height: 500, width: 500 }}/></View>
              : <Image source={{ uri: 'https://www.gotronic.fr/ori-carte-iot-azure-sphere-mt3620-28460.jpg' }} style={{ height: 500, width: 500 }}/>}
          </View>
          <View style={[styles.flex2, styles.w100, styles.alignCenter]}>
            { photo && <PrimaryButton title='Valider' style={[styles.w80, styles.mb20]} isLoading={handleUpload.isLoading} onPress={() => { handleUpload.mutate(photo.uri) }} /> }
            { !handleUpload.isLoading && <PrimaryButton title={photo ? 'ReScanner' : 'Scanner'} style={styles.w80} onPress={checkPerm} />}
          </View>
        </View>
      }
    </View>

  )
}

export default ScanModule
