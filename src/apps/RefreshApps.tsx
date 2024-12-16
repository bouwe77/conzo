import React from 'react'
import { useApplicationsCache } from './ApplicationsCacheContext.js'
import { useView } from '../ViewContext.js'

// Indicates to the app cache that it needs to refresh the apps.

export const RefreshApps = () => {
  const { setNeedsRefresh } = useApplicationsCache()
  const { goToView } = useView()

  React.useEffect(() => {
    setNeedsRefresh(true)
    goToView('search')
  }, [])

  return null
}
