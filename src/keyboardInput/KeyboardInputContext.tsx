import { useInput } from 'ink'
import React from 'react'
import type { PropsWithChildren } from 'react'
import {
  convertToKeyboardInput,
  type KeyboardInput,
} from './convertToKeyboardInput'

interface KeyboardInputContextProps {
  registerHandler: (keyboardInput: KeyboardInput, callback: () => void) => void
  unregisterHandler: (keyboardInput: KeyboardInput) => void
}

const KeyboardInputContext = React.createContext<
  KeyboardInputContextProps | undefined
>(undefined)

export const KeyboardInputProvider = ({ children }: PropsWithChildren) => {
  const [handlers, setHandlers] = React.useState(new Map<string, () => void>())

  const registerHandler = React.useCallback(
    (keyboardInput: KeyboardInput, callback: () => void) => {
      setHandlers((prevHandlers) => {
        const newHandlers = new Map(prevHandlers)
        newHandlers.set(JSON.stringify(keyboardInput), callback)
        return newHandlers
      })
    },
    [],
  )

  const unregisterHandler = React.useCallback(
    (keyboardInput: KeyboardInput) => {
      setHandlers((prevHandlers) => {
        const newHandlers = new Map(prevHandlers)
        newHandlers.delete(JSON.stringify(keyboardInput))
        return newHandlers
      })
    },
    [],
  )

  // When Ink detects keyboard input, call the handler for that input if it exists
  useInput((input, key) => {
    const keyboardInput = convertToKeyboardInput(input, key)
    const handler = handlers.get(keyboardInput)
    if (handler) handler()
  })

  return (
    <KeyboardInputContext.Provider
      value={{ registerHandler, unregisterHandler }}
    >
      {children}
    </KeyboardInputContext.Provider>
  )
}

const useKeyboardInputContext = () => {
  const context = React.useContext(KeyboardInputContext)
  if (context === undefined) {
    throw new Error(
      'useKeyboardInputContext must be used within an KeyboardInputProvider',
    )
  }

  return context
}

export const useKeyboard = (
  keyboardInput: KeyboardInput,
  callback: () => void,
) => {
  const { registerHandler, unregisterHandler } = useKeyboardInputContext()

  const stableKeyboardInput = React.useMemo(
    () => keyboardInput,
    [JSON.stringify(keyboardInput)],
  )
  const stableCallback = React.useCallback(callback, [])

  React.useEffect(() => {
    registerHandler(keyboardInput, callback)
    return () => unregisterHandler(keyboardInput)
  }, [registerHandler, unregisterHandler, stableKeyboardInput, stableCallback])
}

// Convenience hook for the escape key, as it is a global key that you might want to override
// in child components
export const useEscapeKey = (callback: () => void) => {
  useKeyboard(
    {
      key: 'escape',
    },
    callback,
  )
}
