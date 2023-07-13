import { StyleSheet } from 'react-native'
import buttonStyles from './buttonStyles'
import inputStyles from './inputStyles'
import border from './utilities/border'
import display from './utilities/display'
import flex from './utilities/flex'
import sizing from './utilities/sizing'
import spacing from './utilities/spacing'
import text from './utilities/text'

const styles = StyleSheet.create({
  ...sizing,
  ...text,
  ...inputStyles,
  ...buttonStyles,
  ...flex,
  ...display,
  ...spacing,
  ...border
})

export default styles
