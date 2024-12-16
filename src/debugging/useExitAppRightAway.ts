import { useApp } from 'ink'
import { useEffect } from 'react'

// Helper for exiting the app right away, for debugging purposes, for example when debugging styling issues.
export const useExitAppRightAway = () => {
  const { exit } = useApp()

  useEffect(() => {
    exit()
  }, [])
}
