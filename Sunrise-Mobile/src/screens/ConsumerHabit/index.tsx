/* eslint-disable @typescript-eslint/restrict-plus-operands */
import { View, Text, ActivityIndicator } from 'react-native'
import React from 'react'
import ItemList from '@app/components/general/ItemList'
import { consumerStyle } from './style'
import { useQuery } from 'react-query'
import { estimation } from '@app/services/consumption.service'
import styles from '@app/styles'
import colors from '@app/styles/colors'

const ConsumerHabitScreen = (): React.ReactElement => {
  const { data, isLoading } = useQuery('estimation', estimation)

  const getDate = (hours): string => {
    const newDate = new Date('2000-01-01T' + hours) // Conversion en vrai type date
    return `${newDate.getHours().toString()} h ${newDate.getMinutes() < 10 ? `0${newDate.getMinutes().toString()}` : newDate.getMinutes().toString()}`
  }

  return (
    <View style={[styles.w100]}>
      <View style={[styles.alignCenter]}>
        {isLoading
          ? <ActivityIndicator size='large' color={colors.primary} />
          : <View>
            <View style={[consumerStyle.blueCard]}>
              <View style={consumerStyle.consommationCard}>
                <Text style={[consumerStyle.font20]}>Votre consommation estimé :</Text>
                <Text style={[consumerStyle.kilowatt]}>{ data?.estimatedConsumption } KW/h</Text>
              </View>
            </View>
            <View style={[consumerStyle.container]}>
              {data?.startDate !== null
                ? <View>
                  <ItemList content="Vous n'êtes pas en télétravail" />
                  <ItemList content={`Vous travaillez de ${getDate(data?.startDate)} à ${getDate(data?.endDate)} `} />
                </View>
                : <ItemList content="Vous êtes en télétravail" />
              }
              <ItemList content={`Vous êtes ${data?.peopleNumber.toString() ?? 0} dans votre foyer`}></ItemList>
              {
                data?.device && data?.device?.length > 0
                  ? <ItemList content="Vous possédez plusieurs équipements" devices={data?.device} />
                  : <ItemList content="Vous n'avez pas d'équipements éléctroménager"></ItemList>
              }
              <ItemList content={`Vous faites ${data?.washingNumber.toString() ?? 0} machines par semaine`} />
            </View>
          </View>
        }
      </View>
    </View>
  )
}

export default ConsumerHabitScreen
