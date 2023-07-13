import { type GraphData } from '@app/core/interfaces/consuption.interface'
import colors from '@app/styles/colors'
import React from 'react'
import { VictoryAxis, VictoryBar, VictoryChart, VictoryGroup, VictoryLine } from 'victory-native'

interface props {
  dataConsumption?: GraphData[]
  productionData?: GraphData[]
}

const Chart = ({ dataConsumption, productionData }: props): React.ReactElement => {
  return (
    <VictoryChart
      height={220}
      padding={{ left: 40, bottom: 30, top: 10, right: 10 }}
      domainPadding={{ x: 8 }}
    >
      <VictoryAxis
        style={{
          tickLabels: { fontSize: 7 }
        }}
      />
      <VictoryAxis
        dependentAxis
        orientation="left"
        style={{ tickLabels: { fontSize: 10 } }}
        label="KW"
      />
      <VictoryGroup offset={20}
        animate={{
          duration: 1000,
          onLoad: { duration: 1000 }
        }}
        colorScale={[colors.secondary]}
      >
        <VictoryBar
          data={dataConsumption?.map(el => ({ x: el.time, y: el.value }))}
        />
      </VictoryGroup>
      <VictoryLine
        style={{
          data: {
            stroke: '#EE1999'
          }
        }}
        interpolation="natural"
        animate={{
          duration: 1000,
          onLoad: { duration: 1000 }
        }}
        data={productionData?.map(el => ({ x: el.time, y: el.value }))}
      />
    </VictoryChart>
  )
}

export default Chart
