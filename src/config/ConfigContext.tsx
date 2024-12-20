import React from 'react'
import type { Config } from './types.js'

type ConfigContextType = Config

const Config = React.createContext<ConfigContextType | null>(null)

type Props = {
  children: React.ReactNode
  config: ConfigContextType
}

function ConfigProvider({ children, config }: Props) {
  return <Config.Provider value={config}>{children}</Config.Provider>
}

function useConfig() {
  const context = React.useContext(Config)
  if (!context)
    throw new Error('useConfig must be used within a ConfigProvider')
  return context
}

export { ConfigProvider, useConfig }
