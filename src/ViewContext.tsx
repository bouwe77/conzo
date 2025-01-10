import React from 'react'
import type { ReactItem } from './items/types.js'

type ErrorView = {
  error: Error
}

type View = 'search' | 'loading' | ErrorView | ReactItem

type ViewContextType = {
  view: View
  goToView: (view: View) => void
}

const ViewContext = React.createContext<ViewContextType | null>(null)

const ViewProvider = ({ children }: React.PropsWithChildren) => {
  const [view, setView] = React.useState<View>('search')

  const goToView = React.useCallback((view: View) => {
    setView(view)
  }, [])

  const value = { view, goToView }

  return <ViewContext.Provider value={value}>{children}</ViewContext.Provider>
}

const useView = () => {
  const context = React.useContext(ViewContext)
  if (!context) throw new Error('useView must be used within a ViewProvider')
  return context
}

export const useBackToSearch = () => {
  const { goToView } = useView()

  return React.useCallback(() => {
    goToView('search')
  }, [goToView])
}

export { ViewProvider, useView }
