import { Box, Text, useInput } from 'ink'
import React from 'react'
import { useConfig } from './config/ConfigContext.js'

const noop = () => {}

export const SelectBox = ({
  items,
  maxVisibleResults = 10,
  onChange = noop,
  onSelect,
  width,
}: {
  items: string[]
  maxVisibleResults?: number
  onChange?: (item: string) => void
  onSelect: (item: string) => void
  width?: number
}) => {
  const [selectedIndex, setSelectedIndex] = React.useState(0)
  const [startIndex, setStartIndex] = React.useState(0)
  const config = useConfig()

  const select = (which: 'previous' | 'next') => {
    let newSelectedIndex =
      which === 'previous'
        ? (selectedIndex - 1 + items.length) % items.length
        : (selectedIndex + 1) % items.length

    setSelectedIndex(newSelectedIndex)
    setStartIndex(Math.max(0, newSelectedIndex - maxVisibleResults + 1))

    return newSelectedIndex
  }

  useInput((_, key) => {
    if (items.length === 0 || !items[selectedIndex]) return

    if (key.return) {
      onSelect(items[selectedIndex])
    }
    if (key.upArrow) {
      const newSelectedIndex = select('previous')
      if (items[newSelectedIndex]) onChange(items[newSelectedIndex])
    }
    if (key.downArrow) {
      const newSelectedIndex = select('next')
      if (items[newSelectedIndex]) onChange(items[newSelectedIndex])
    }
  })

  if (items.length === 0) return null

  // 4 is some padding, and spaces at the beginning and end of the item
  // const width = items.reduce((max, item) => Math.max(max, item.length), 0) + 4

  if (!width) {
    width = items.reduce((max, item) => Math.max(max, item.length), 0) + 4
  }

  return (
    <Box
      borderStyle="round"
      borderColor="grey"
      flexDirection="column"
      width={width}
    >
      {items.slice(startIndex, startIndex + maxVisibleResults).map((item) => {
        const selected = item === items[selectedIndex]
        // 3 is an arbitrary number of some spaces around the item name, and some built-in padding.
        let name = ` ${item}${' '.repeat(width > 0 ? width - 3 - item.length : 0)}`

        return (
          <Box key={item}>
            <Text
              backgroundColor={selected ? config.theme.color : undefined}
              bold={selected}
            >
              {name}
            </Text>
          </Box>
        )
      })}
    </Box>
  )
}
