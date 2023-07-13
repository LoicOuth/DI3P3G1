import * as React from 'react'
import Svg, { G, Path, Defs } from 'react-native-svg'
/* SVGR has dropped some elements not supported by react-native-svg: filter */

const HomeElecSvg = (props): React.ReactElement => (
  <Svg
    width={71}
    height={68}
    fill="none"
    xmlns="http://www.w3.org/2000/svg"
    {...props}
  >
    <G filter="url(#a)">
      <Path
        d="m59.188 25.976-.211 41.38-47.29-.242.21-41.38"
        stroke="#fff"
        strokeWidth={7}
        strokeMiterlimit={10}
      />
    </G>
    <Path
      d="M68.025 31.933 35.648 5.166 3 31.6"
      stroke="#fff"
      strokeWidth={7}
      strokeMiterlimit={10}
    />
    <G filter="url(#b)">
      <Path
        d="m44.35 37.724-8.868-.046.046-8.867L26.6 40.59l8.867.045-.045 8.867 8.927-11.777Z"
        stroke="#fff"
        strokeWidth={7}
        strokeMiterlimit={10}
      />
    </G>
    <Defs></Defs>
  </Svg>
)

export default HomeElecSvg
