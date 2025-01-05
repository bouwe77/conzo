import type { Key as InkKey } from 'ink'

const keys = [
  'escape',
  'backspace',
  'ctrl',
  'delete',
  'downArrow',
  'leftArrow',
  'meta',
  'pageDown',
  'pageUp',
  'return',
  'rightArrow',
  'shift',
  'tab',
  'upArrow',
] as const

type Key = (typeof keys)[number]

export type KeyboardInput =
  | {
      input: string
      key: Key
    }
  | {
      key: Key
    }
  | {
      input: string
    }

// Converts Ink's input and key to a Conzo KeyboardInput object
export const convertToKeyboardInput = (input: string, key: InkKey) => {
  const rawInput = {
    input,
    key: keys.find((k) => key[k]) || '',
  }

  const keyboardInput: KeyboardInput =
    rawInput.input && rawInput.key
      ? { input: rawInput.input, key: rawInput.key as Key }
      : rawInput.input
        ? { input: rawInput.input }
        : rawInput.key
          ? { key: rawInput.key as Key }
          : (undefined as never)

  return JSON.stringify(keyboardInput)
}
