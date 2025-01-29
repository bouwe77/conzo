import React, { type PropsWithChildren } from 'react'
import { useBackToSearch, useView } from '../ViewContext.js'
import { Box, Text } from 'ink'
import { useEscapeKey } from '../keyboardInput/KeyboardInputContext.js'

export const EscapeBackToSearch = ({ children }: PropsWithChildren) => {
  const backToSearch = useBackToSearch()
  useEscapeKey(backToSearch)

  return (
    <>
      {children}

      <Box borderStyle="round" borderColor="grey">
        <Text>ESC = back</Text>
      </Box>
    </>
  )
}
