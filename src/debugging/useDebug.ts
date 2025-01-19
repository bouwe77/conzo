import React from 'react'
import { useConfig } from '../config/ConfigContext'

export const useDebug = () => {
  const logToConsole = React.useCallback(
    (message: string | object) =>
      typeof message === 'object' ? console.dir(message) : console.log(message),
    [],
  )

  const noop = React.useCallback(() => {}, [])

  const { debug } = useConfig()

  return debug ? logToConsole : noop
}
