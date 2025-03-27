import React from 'react'
import { Box, Text } from 'ink'
import { version } from './version.js'

export const Footer = () => (
  <Box justifyContent="flex-end">
    <Text>conzo {version} </Text>
  </Box>
)
