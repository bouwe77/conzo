import React from 'react'
import { useInput } from 'ink'
import { useView } from '../ViewContext.js'
import { Box, Text } from 'ink'
import { useConfig } from '../config/ConfigContext.js'

type Props = {
  title: string
  children: React.ReactNode
}

export const EscapeBackToSearch = ({ title, children }: Props) => {
  const { goToView } = useView()
  const config = useConfig()

  useInput((_, key) => {
    if (key.escape) goToView('search')
  })

  return (
    <>
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
      <Box borderStyle="round" borderColor="grey">
        <Text>ESC = back</Text>
      </Box>
    </>
  )
}
