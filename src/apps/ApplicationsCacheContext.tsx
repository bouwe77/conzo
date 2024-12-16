import React from 'react'

type ApplicationsCacheContextType = {
  needsRefresh: boolean
  setNeedsRefresh: (needsRefresh: boolean) => void
}

const ApplicationsCache =
  React.createContext<ApplicationsCacheContextType | null>(null)

function ApplicationsCacheProvider({ children }: React.PropsWithChildren) {
  const [needsRefresh, setNeedsRefresh] = React.useState<boolean>(false)

  const value = { needsRefresh, setNeedsRefresh }

  return (
    <ApplicationsCache.Provider value={value}>
      {children}
    </ApplicationsCache.Provider>
  )
}

function useApplicationsCache() {
  const context = React.useContext(ApplicationsCache)
  if (!context)
    throw new Error(
      'useApplicationsCache must be used within a ApplicationsCacheProvider',
    )
  return context
}

export { ApplicationsCacheProvider, useApplicationsCache }
