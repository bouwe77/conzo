import { createContext, useContext, useState } from 'react'
import type { PropsWithChildren } from 'react'

interface EscapeContextProps {
  escapeEnabled: boolean
  setEscapeEnabled: (enabled: boolean) => void
}

const EscapeContext = createContext<EscapeContextProps | undefined>(undefined)

export const EscapeBackToSearchProvider = ({ children }: PropsWithChildren) => {
  const [escapeEnabled, setEscapeEnabled] = useState<boolean>(true)

  return (
    <EscapeContext.Provider value={{ escapeEnabled, setEscapeEnabled }}>
      {children}
    </EscapeContext.Provider>
  )
}

const useEscapeBackToSearchContext = () => {
  const context = useContext(EscapeContext)
  if (context === undefined) {
    throw new Error(
      'useEscape must be used within an EscapeBackToSearchProvider',
    )
  }

  return context
}

// Determine if the escape key to return to search is enabled
export const useEscapeBackToSearch = () => {
  const { escapeEnabled } = useEscapeBackToSearchContext()
  return escapeEnabled
}

// Enable or disable the escape key to return to search
export const useEnableBackToSearch = (enable: boolean) => {
  const { setEscapeEnabled } = useEscapeBackToSearchContext()
  setEscapeEnabled(enable)
}
