import React, { useEffect, useState } from 'react'
import { Box, Text } from 'ink'
import childProcess from 'child_process'
import { tryCatch } from './helpers/tryCatch.js'

// Backups items config and other private stuff as this is git ignored.

type Status = 'Loading...' | 'Done!' | { type: 'Failed'; error: string }

export const ConfirmStatus = ({ command }: { command: string }) => {
  const [status, setStatus] = useState<Status>('Loading...')

  useEffect(() => {
    const { error } = tryCatch(() => childProcess.execSync(command).toString())

    if (error) {
      setStatus({ type: 'Failed', error: error.message })
    } else {
      setStatus('Done!')
    }
  }, [command])

  return (
    <Box>
      <Text>{typeof status === 'string' ? status : status.type}</Text>
      {typeof status === 'object' && <Text color="red">{status.error}</Text>}
    </Box>
  )
}
