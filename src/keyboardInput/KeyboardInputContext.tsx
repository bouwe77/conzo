import { useInput } from 'ink'
import React from 'react'
import type { PropsWithChildren } from 'react'
import {
  convertToKeyboardInput,
  type KeyboardInput,
} from './convertToKeyboardInput'

type StringifiedKeyboardInput = string
type Callback = () => void

interface KeyboardInputContextProps {
  registerHandler: (keyboardInput: KeyboardInput, callback: Callback) => void
  unregisterHandler: (keyboardInput: KeyboardInput) => void
}

const KeyboardInputContext = React.createContext<
  KeyboardInputContextProps | undefined
>(undefined)

export const KeyboardInputProvider = ({ children }: PropsWithChildren) => {
  const [handlers, setHandlers] = React.useState<
    Map<StringifiedKeyboardInput, Callback[]>
  >(new Map())

  const registerHandler = React.useCallback(
    (keyboardInput: KeyboardInput, callback: Callback) => {
      setHandlers((prevHandlers) => {
        const newHandlers = new Map(prevHandlers)
        const key = JSON.stringify(keyboardInput)
        const stack = newHandlers.get(key) || []
        newHandlers.set(key, [...stack, callback])
        return newHandlers
      })
    },
    [],
  )

  const unregisterHandler = React.useCallback(
    (keyboardInput: KeyboardInput) => {
      setHandlers((prevHandlers) => {
        const newHandlers = new Map(prevHandlers)
        const key = JSON.stringify(keyboardInput)
        const stack = newHandlers.get(key)
        if (stack) {
          stack.pop()
          if (stack.length === 0) {
            newHandlers.delete(key)
          } else {
            newHandlers.set(key, stack)
          }
        }
        return newHandlers
      })
    },
    [],
  )

  // When Ink detects keyboard input, call the last handler for that input if it exists
  useInput((input, key) => {
    const keyboardInput = convertToKeyboardInput(input, key)
    const stack = handlers.get(JSON.stringify(keyboardInput))
    stack?.[stack.length - 1]?.()
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
  callback: Callback,
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
export const useEscapeKey = (callback: Callback) => {
  useKeyboard(
    {
      key: 'escape',
    },
    callback,
  )
}
