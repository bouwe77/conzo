import React from 'react'
import type { BookmarkItem, Item, ReactItem } from './types.js'
import type { Config } from '../config/types.js'
import { RefreshApps } from '../apps/RefreshApps.js'
import { spawnProcess } from '../helpers/spawnProcess.js'
import { homedir } from 'node:os'
import * as path from 'node:path'

const createPathResolver =
  (parentDir: string) =>
  (...parts: string[]) => {
    return path.resolve(parentDir, ...parts)
  }

const home = (...pathParts: string[]) => {
  return createPathResolver(homedir())(...pathParts)
}

const parseAppList = (data: Buffer, excludeApps: string[]) => {
  const apps = data
    .toString()
    .split('\n')
    .filter((app) => app.endsWith('.app'))
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

// Fetch installed apps using the `ls` command
const getApplications = async (excludeApps: string[]): Promise<Item[]> => {
  const data = await spawnProcess('ls', ['/Applications'])
  return parseAppList(data, excludeApps)
}

// Fetch system applications using the `ls` command
const getSystemApplications = async (
  excludeApps: string[],
): Promise<Item[]> => {
  const data = await spawnProcess('ls', ['/System/Applications'])
  return parseAppList(data, excludeApps)
}

const getSystemUtilities = async (excludeApps: string[]): Promise<Item[]> => {
  const data = await spawnProcess('ls', ['/System/Applications/Utilities'])
  return parseAppList(data, excludeApps)
}

const getChromeAppsLocalized = async (
  excludeApps: string[],
): Promise<Item[]> => {
  const data = await spawnProcess('ls', [
    home('Applications', 'Chrome Apps.localized'),
  ])
  return parseAppList(data, excludeApps)
}

// Fetch installed preference panes using the `find` command
const getPrefPanes = async (): Promise<Item[]> => {
  const data = await spawnProcess('find', [
    '/System/Library/PreferencePanes',
    '-name',
    '*.prefPane',
  ])

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
  const [apps, systemApps, systemUtilities, chromeApps, prefPanes] =
    await Promise.all([
      getApplications(excludeApps),
      getSystemApplications(excludeApps),
      getSystemUtilities(excludeApps),
      getChromeAppsLocalized(excludeApps),
      getPrefPanes(),
    ])

  return [
    ...apps,
    ...prefPanes,
    ...systemApps,
    ...systemUtilities,
    ...chromeApps,
  ]
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
