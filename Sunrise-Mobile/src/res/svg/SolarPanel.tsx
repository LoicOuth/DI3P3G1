import * as React from 'react'
import Svg, { G, Path, Defs } from 'react-native-svg'

const SolarPanel = (props): React.ReactElement => (
  <Svg
    width={77}
    height={63}
    fill="none"
    xmlns="http://www.w3.org/2000/svg"
    {...props}
  >
    <G filter="url(#a)">
      <Path
        d="m50.573 48.126-5.172.005v-3.444h-13.8v3.46l-5.173.006c-.95.001-1.721.77-1.722 1.718l-.006 3.408A1.722 1.722 0 0 0 26.427 55l24.146-.028a1.722 1.722 0 0 0 1.723-1.717l.004-3.408a1.722 1.722 0 0 0-1.727-1.72ZM67.092 2.872C66.809 1.215 65.344 0 63.627 0H13.372c-1.717 0-3.182 1.215-3.465 2.872-6.265 36.784-5.902 34.52-5.903 34.94C4 39.673 5.544 41.25 7.518 41.25h61.964c1.965 0 3.505-1.564 3.514-3.415.002-.438.36 1.82-5.904-34.963ZM32.013 6.875h12.973l1.053 10.313H30.96l1.054-10.313Zm-8.104 27.5H11.665l2.05-12.031h11.424l-1.23 12.031Zm1.756-17.188H14.592l1.756-10.312h10.37l-1.053 10.313Zm3.538 17.188 1.23-12.031h16.134l1.229 12.031H29.203Zm21.078-27.5h10.37l1.756 10.313H51.334L50.281 6.874Zm2.81 27.5-1.23-12.031h11.425l2.05 12.031H53.09Z"
        fill="#fff"
      />
    </G>
    <Defs></Defs>
  </Svg>
)

export default SolarPanel
