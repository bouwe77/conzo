import React from 'react'
import { Box, measureElement, Text } from 'ink'
import { Search } from './Search.js'
import { Component } from './Component.js'
import type { Item, ReactItem } from './items/types.js'
import { useView } from './ViewContext.js'
import { Error } from './Error.js'
import { Footer } from './Footer.js'
import { useConfig } from './config/ConfigContext.js'

export default function App() {
  const { view, goToView } = useView()

  const { maxItemsVisible } = useConfig()

  // The searchKey is used to force a re-render of the Search component
  const [searchKey, setSearchKey] = React.useState(0)

  const [containerWidth, setContainerWidth] = React.useState(0)
  const containerRef = React.useRef(null)

  React.useEffect(() => {
    if (!containerRef.current) return
    const { width } = measureElement(containerRef.current)
    setContainerWidth(width)
  }, [])

  React.useEffect(() => {
    if (view === 'loading') {
      setTimeout(() => {
        renderSearch()
      }, 3000)
    }
  }, [view])

  const renderSearch = () => {
    setSearchKey((prev) => prev + 1)
    goToView('search')
  }

  const choose = async (item: Item) => {
    if (item.actionType === 'Show UI') {
      try {
        goToView(item as ReactItem)
      } catch (error) {
        goToView({ error: error as Error })
      }
    } else if (item.actionType === 'Fire and forget') {
      try {
        await item.action()
        goToView('loading')
      } catch (error) {
        goToView({ error: error as Error })
      }
    }
  }

  return (
    <Box flexDirection="column">
      <Box
        flexDirection="column"
        borderStyle="round"
        borderColor="grey"
        width="100%"
        minHeight={maxItemsVisible + 6}
        ref={containerRef}
      >
        {view === 'search' ? (
          <Search key={searchKey} width={containerWidth} choose={choose} />
        ) : view === 'loading' ? (
          <Text>Please wait...</Text>
        ) : 'error' in view ? (
          <Error error={view.error} />
        ) : 'actionType' in view && view.actionType === 'Show UI' ? (
          <Component item={view} />
        ) : null}
      </Box>
      <Footer />
    </Box>
  )
}
