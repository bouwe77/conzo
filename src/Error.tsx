import React from 'react'
import { EscapeBackToSearch } from './helpers/EscapeBackToSearch.js'
import type { FallbackProps } from 'react-error-boundary'
import { Box, Text, useInput } from 'ink'
import { Container } from './Container.js'

export const Error = ({ error }: { error: Error }) => {
  const [showStack, setShowStack] = React.useState(false)

  // Note: This useInput sometimes conflicts with the parent component's useInput...
  useInput((input) => {
    if (input === 's') {
      setShowStack((prev) => !prev)
    }
  })

  return (
    <EscapeBackToSearch>
      <Container title="Oops...">
        <Text>{error.message}</Text>
        <Box marginTop={1}>
          {showStack ? (
            <Text>{error.stack}</Text>
          ) : (
            <Text>S = show stack trace</Text>
          )}
        </Box>
      </Container>
    </EscapeBackToSearch>
  )
}

// Standalone Error component
export const Fallback = ({ error }: FallbackProps) => {
  // Call resetErrorBoundary() to reset the error boundary and retry the render.

  return <Text>{(error as unknown as Error).message}</Text>
}
