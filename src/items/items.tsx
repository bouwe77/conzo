import React from 'react'
import type { BookmarkItem, Item, ReactItem } from './types.js'
import type { Config } from '../config/types.js'
import { RefreshApps } from '../apps/RefreshApps.js'
import { spawnProcess } from '../helpers/spawnProcess.js'

// Fetch installed apps using the `ls` command
const getApplications = async (excludeApps: string[]): Promise<Item[]> => {
  const data = await spawnProcess('ls', ['/Applications'])

  const apps = data
    .toString()
    .split('\n')
    .map((app) => app.replace('.app', '').trim())
    .filter((app) => app !== '' && !excludeApps.includes(app))

  const items = apps.map((app) => ({
    name: app,
    action: async () => {
      await spawnProcess('open', ['-a', app])
    },
    actionType: 'Fire and forget',
  })) as Item[]

  return items
}

// Fetch installed preference panes using the `find` command
const getPrefPanes = async (): Promise<Item[]> => {
  // Somehow this command does not return anything...
  // const data = await spawnProcess('find', [
  //   '/System/Library/PreferencePanes',
  //   '-name',
  //   '"*.prefPane"',
  // ])

  // ScriptKit lists the following applications:
  // const APP_DIR = '/Applications'
  // const UTILITIES_DIR = `${APP_DIR}/Utilities`
  // const SYSTEM_UTILITIES_DIR = '/System/Applications/Utilities'
  // const CHROME_APPS_DIR = home('Applications', 'Chrome Apps.localized')

  // as long as the command does not work, here is a hardcoded list with the most common prefPanes
  const data = `
  /System/Library/PreferencePanes/Bluetooth.prefPane
  /System/Library/PreferencePanes/Network.prefPane
  /System/Library/PreferencePanes/Battery.prefPane
  /System/Library/PreferencePanes/Security.prefPane
  /System/Library/PreferencePanes/Dock.prefPane
  /System/Library/PreferencePanes/Sound.prefPane
  /System/Library/PreferencePanes/Appearance.prefPane
  /System/Library/PreferencePanes/Displays.prefPane
  /System/Library/PreferencePanes/Notifications.prefPane
  /System/Library/PreferencePanes/Accounts.prefPane
  /System/Library/PreferencePanes/Trackpad.prefPane
  /System/Library/PreferencePanes/DateAndTime.prefPane
  /System/Library/PreferencePanes/EnergySaverPref.prefPane
  /System/Library/PreferencePanes/Keyboard.prefPane
  /System/Library/PreferencePanes/Spotlight.prefPane
  /System/Library/PreferencePanes/AppleIDPrefPane.prefPane
  /System/Library/PreferencePanes/SharingPref.prefPane
  /System/Library/PreferencePanes/Profiles.prefPane
  /System/Library/PreferencePanes/SoftwareUpdate.prefPane
  /System/Library/PreferencePanes/Mouse.prefPane
  `

  const prefPanes = data
    .toString()
    .split('\n')
    .map((prefPane) => prefPane.trim())
    .filter((prefPane) => prefPane !== '')

  const items = prefPanes.map((prefPane) => ({
    name: prefPane.split('/').pop()?.replace('.prefPane', '').trim() || '',
    action: async () => {
      await spawnProcess('open', [prefPane])
    },
    actionType: 'Fire and forget',
  })) as Item[]

  return items
}

const getCachableItems = async (excludeApps: string[]): Promise<Item[]> => {
  const [apps, prefPanes] = await Promise.all([
    getApplications(excludeApps),
    getPrefPanes(),
  ])

  return [...apps, ...prefPanes]
}

const getDefaultUIs = (): ReactItem[] => [
  {
    name: 'Refresh apps',
    action: () => <RefreshApps />,
    actionType: 'Show UI',
  },
]

const getDefaultBookmarks = (): BookmarkItem[] => []

// Main function to get items, with caching support for apps only
export const getItems = (() => {
  let cachedItems: Item[] | null = null

  return async ({
    refresh = false,
    config,
  }: {
    refresh: boolean
    config: Config
  }): Promise<Item[]> => {
    if (!cachedItems || refresh)
      cachedItems = await getCachableItems(config.excludeApps)

    const defaultItems = [
      ...getDefaultUIs(),
      ...getDefaultBookmarks(),
    ] as Item[]

    const items = [...cachedItems, ...defaultItems, ...config.items]

    return items.sort((a, b) => {
      const isAFavorite = config.favoriteItems.includes(a.name)
      const isBFavorite = config.favoriteItems.includes(b.name)

      if (isAFavorite && !isBFavorite) return -1
      if (!isAFavorite && isBFavorite) return 1
      return a.name.localeCompare(b.name)
    })
  }
})()
