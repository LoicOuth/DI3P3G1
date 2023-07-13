import Chart from '@app/components/home/Chart'
import EquipmentItem from '@app/components/home/EquipmentItem'
import InfoItem from '@app/components/home/InfoItem'
import Meteo from '@app/components/home/Meteo'
import useAuth from '@app/hooks/useAuth'
import FlashSvg from '@app/res/svg/FlashSvg'
import HomeElecSvg from '@app/res/svg/HomeElecSvg'
import HotWaterSvg from '@app/res/svg/HotWaterSvg'
import SolarPanel from '@app/res/svg/SolarPanel'
import { getConsumption } from '@app/services/consumption.service'
import { getChauffeEau } from '@app/services/iot.service'
import { getMeteo } from '@app/services/meteo.service'
import { getAutonom, getProductionYersteday } from '@app/services/production.service'
import styles from '@app/styles'
import colors from '@app/styles/colors'
import { LinearGradient } from 'expo-linear-gradient'
import React from 'react'
import { Text, View, ActivityIndicator } from 'react-native'
import { useQuery } from 'react-query'

const DashboardScreen = (): React.ReactElement => {
  const { authData } = useAuth()
  const { data: consumptionData, isLoading } = useQuery(
    'consumptionData',
    async () => await getConsumption(false)
  )

  const { data: productionData, isLoading: loadingProd } = useQuery('productionData', getProductionYersteday)
  const { data: currentProd } = useQuery('currentProd', getAutonom, {
    refetchInterval: 20000
  })
  const { data: meteo } = useQuery('meteo', getMeteo, {
    refetchInterval: 20000
  })
  const { data: chauffeEau, isLoading: loadingCE } = useQuery('currentChauffe', getChauffeEau, {
    refetchInterval: 20000
  })

  return (
    <View style={styles.flex1}>

      <LinearGradient colors={['#1B61B6', 'rgba(0, 173, 239, 0.2)']} style={[styles.flex4]}>
        <View style={[styles.flex1, styles.w100, styles.flexRow, styles.justifyBetween, styles.p15]}>
          <Text style={[styles.textXl, styles.textWhite, styles.w70]}>Ravie de vous revoir {authData?.user?.firstName}</Text>
          {meteo && <Meteo _meteo={meteo} />}
        </View>

        {isLoading || loadingProd
          ? <ActivityIndicator size='large' color={colors.primary} />
          : <View style={styles.flex5}>
            {
              currentProd && <View style={[styles.flex2, styles.w100, styles.flexRow, styles.justifyEvenly, styles.alignCenter]}>
                <InfoItem icon={<SolarPanel />} value={currentProd?.productionInstantane.toFixed(2)} unit='W' />
                <InfoItem icon={<HomeElecSvg />} value={currentProd?.consommationInstantane.toFixed(2)} unit='W' />
                <InfoItem icon={<FlashSvg />} value={currentProd?.tauxAutonomie.toFixed(2)} unit='%' />
              </View>
            }
            <View style={[styles.mt20, { flex: 0.7 }]}>
              <View style={[styles.m10, styles.bb1, styles.borderWhite]}>
                <Text style={[styles.textMd, styles.textWhite]}>Production et consommation des dernières 24 heures</Text>
              </View>
            </View>
            <View style={{ flex: 3, width: '100%' }}>
              <Chart dataConsumption={consumptionData} productionData={productionData}/>
            </View>
          </View>
        }
      </LinearGradient>

      <View style={[styles.flex2, styles.alignStart, { backgroundColor: 'white' }]}>
        <View style={[styles.m10, styles.bb1, styles.borderGray]}>
          <Text style={[styles.textMd, styles.textGray]}>Gérer mes équipements connectés</Text>
        </View>
        {loadingCE
          ? <ActivityIndicator size='large' color={colors.primary} />
          : <View style={styles.ml20}>
            <EquipmentItem text='Chauffe eau' icon={<HotWaterSvg />} status={chauffeEau} />
          </View>
        }

      </View>

    </View>
  )
}

export default DashboardScreen
