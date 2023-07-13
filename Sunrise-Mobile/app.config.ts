import * as dotenv from 'dotenv'
dotenv.config()

export default (): any => ({
  expo: {
    name: 'MobileApp',
    slug: 'MobileApp',
    version: '1.0.0',
    orientation: 'portrait',
    icon: './assets/sunrise_logo.png',
    userInterfaceStyle: 'light',
    splash: {
      image: './assets/splash.png',
      resizeMode: 'contain',
      backgroundColor: '#ffffff'
    },
    updates: {
      fallbackToCacheTimeout: 0
    },
    assetBundlePatterns: [
      '**/*'
    ],
    ios: {
      supportsTablet: true
    },
    android: {
      adaptiveIcon: {
        foregroundImage: './assets/sunrise_logo.png',
        backgroundColor: '#FFFFFF'
      },
      package: 'com.MobileApp',
      versionCode: 1
    },
    web: {
      favicon: './assets/sunrise_logo.png'
    },
    extra: {
      apiUrl: process.env.API_URL,
      eas: {
        projectId: 'b6bff969-445c-46ea-a44b-1325b0444ca7'
      }
    },
    plugins: [
      [
        'expo-camera',
        {
          cameraPermission: 'Allow $(PRODUCT_NAME) to access your camera.'
        }
      ]
    ]
  }

})
