import React from 'react'
import TextInput from 'ink-text-input'
import { Box, Text, useInput } from 'ink'
import { useApplicationsCache } from './apps/ApplicationsCacheContext.js'
import { useConfig } from './config/ConfigContext.js'
import { useView } from './ViewContext.js'
import type { Item } from './items/types.js'
import { getItems } from './items/items.js'

const initialState = {
  allItems: [],
  filteredResults: [],
  selectedItem: null,
  visibleStartIndex: 0,
}

type ResultsState = {
  allItems: Item[]
  filteredResults: Item[]
  selectedItem: Item | null
  visibleStartIndex: number
}

type ResultsAction =
  | {
      type: 'INIT_ITEMS'
      payload: Item[]
    }
  | {
      type: 'SELECT_RESULT'
      which: 'previous' | 'next'
    }
  | {
      type: 'FILTER_RESULTS'
      query: string
    }

const createResultsReducer =
  (maxItemsVisible: number) => (state: ResultsState, action: ResultsAction) => {
    switch (action.type) {
      case 'INIT_ITEMS': {
        const allItems = action.payload
        return {
          ...state,
          allItems,
          filteredResults: allItems,
          selectedItem: allItems[0] ?? null,
        }
      }
      case 'FILTER_RESULTS': {
        const query = action.query.toLowerCase()

        const visibleStartIndex = 0

        if (query === '') {
          const filteredResults = state.allItems
          return {
            ...state,
            filteredResults,
            visibleStartIndex,
            selectedItem: filteredResults[visibleStartIndex] ?? null,
          }
        }

        // Returns true if all characters in `query` appear in `target` in the same order
        const isSubsequence = (query: string, target: string) => {
          let i = 0
          for (const char of target) {
            if (char === query[i]) i++
            if (i === query.length) return true
          }
          return false
        }

        const filteredResults = state.allItems.filter((result) =>
          isSubsequence(query.toLowerCase(), result.name.toLowerCase()),
        )
        return {
          ...state,
          filteredResults,
          visibleStartIndex,
          selectedItem: filteredResults[visibleStartIndex] ?? null,
        }
      }

      case 'SELECT_RESULT': {
        if (state.filteredResults.length === 0 || state.selectedItem === null) {
          return state
        }

        const selectedIndex = state.filteredResults
          .map((r) => r.name)
          .indexOf(state.selectedItem.name)
        let newSelectedIndex =
          action.which === 'previous'
            ? (selectedIndex - 1 + state.filteredResults.length) %
              state.filteredResults.length
            : (selectedIndex + 1) % state.filteredResults.length
        const newSelectedName = state.filteredResults[newSelectedIndex] ?? null

        const start = Math.max(0, newSelectedIndex - maxItemsVisible + 1)

        return {
          ...state,
          selectedItem: newSelectedName,
          visibleStartIndex: start,
        }
      }

      default:
        return state
    }
  }

type Props = {
  width: number
  choose: (item: Item) => void
}

export const Search = ({ width, choose }: Props) => {
  const [query, setQuery] = React.useState('')
  const config = useConfig()
  const [{ filteredResults, visibleStartIndex, selectedItem }, dispatch] =
    React.useReducer(createResultsReducer(config.maxItemsVisible), initialState)
  const { needsRefresh, setNeedsRefresh } = useApplicationsCache()
  const { goToView } = useView()

  React.useEffect(() => {
    const fetchItems = async () => {
      try {
        const items = await getItems({
          refresh: needsRefresh,
          config,
        })
        dispatch({ type: 'INIT_ITEMS', payload: items })
      } catch (error) {
        goToView({ error: error as Error })
      }
    }

    fetchItems()

    if (needsRefresh) setNeedsRefresh(false)
  }, [needsRefresh, config, setNeedsRefresh, goToView])

  useInput((_, key) => {
    if (key.upArrow) dispatch({ type: 'SELECT_RESULT', which: 'previous' })
    if (key.downArrow) dispatch({ type: 'SELECT_RESULT', which: 'next' })
    if (key.escape) filter('')
  })

  const filter = (query: string) => {
    setQuery(query)
    dispatch({ type: 'FILTER_RESULTS', query })
  }

  return (
    <>
      <Box borderStyle="round" borderColor={config.theme.color}>
        <Text> </Text>
        <TextInput
          value={query}
          onChange={filter}
          showCursor
          placeholder="Search..."
          onSubmit={() => {
            if (selectedItem) choose(selectedItem)
          }}
        />
      </Box>
      <Box borderStyle="round" borderColor="grey" flexDirection="column">
        {filteredResults.length === 0 && (
          <Box marginLeft={1}>
            <Text>No items...</Text>
          </Box>
        )}
        {filteredResults
          .slice(visibleStartIndex, visibleStartIndex + config.maxItemsVisible)
          .map((item) => {
            const selected = selectedItem?.name === item.name

            // 5 is an arbitrary number of some spaces around the item name, and some built-in padding.
            let name = ` ${item.name}${' '.repeat(width > 0 ? width - 5 - item.name.length : 0)}`
            if (item.actionType === 'Show UI') name = name.slice(0, -2) + '> '

            return (
              <Box key={item.name}>
                <Text
                  backgroundColor={selected ? config.theme.color : undefined}
                  bold={selected}
                >
                  <Text>{name}</Text>
                </Text>
              </Box>
            )
          })}
      </Box>
    </>
  )
}
