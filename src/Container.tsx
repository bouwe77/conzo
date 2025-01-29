import React, { type PropsWithChildren } from 'react'
import { Box, Text } from 'ink'
import { useConfig } from './config/ConfigContext.js'

interface Props extends PropsWithChildren {
  title: string
}

export const Container = ({ title, children }: Props) => {
  const config = useConfig()

  return (
    <Box
      flexDirection="column"
      borderStyle="round"
      borderColor={config.theme.color}
    >
      <Box marginBottom={1}>
        <Text color={config.theme.color}>{title}</Text>
      </Box>

      {children}
    </Box>
  )
}
