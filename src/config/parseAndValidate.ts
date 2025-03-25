import type { Config, UserConfig, UserItem } from './types.js'
import type { BookmarkAction, Item } from '../items/types.js'
import { openBookmark } from '../GoogleChrome.js'

const defaultConfig: Config = {
  excludeApps: [],
  favoriteItems: [],
  chromeProfiles: false,
  items: [],
  maxItemsVisible: 10,
  theme: {
    color: 'green',
  },
  debug: {
    enabled: false,
  },
}

export const parseAndValidate = (userConfig?: UserConfig): Config => {
  if (!userConfig) return defaultConfig

  const chromeProfiles = userConfig.chromeProfiles || false

  let items: Item[] = []
  if (userConfig.items) {
    items = userConfig.items
      .map((userItem: UserItem) => {
        const actionString = userItem.action.toString()
        // A function that returns JSX is considered a UI action
        if (
          ['() => React.createElement(', '() => _jsx('].some((str) =>
            actionString.startsWith(str),
          )
        )
          return {
            name: userItem.name,
            action: userItem.action as () => JSX.Element,
            actionType: 'Show UI',
          }

        // A function that returns a promise is considered a fire-and-forget action
        if (
          ['() => Promise.resolve(', 'async () =>'].some((str) =>
            actionString.startsWith(str),
          )
        )
          return {
            name: userItem.name,
            action: userItem.action as () => Promise<void>,
            actionType: 'Fire and forget',
          }

        // An object is considered a bookmark object, which is converted to a fire-and-forget actions
        if (actionString === '[object Object]')
          return {
            name: userItem.name,
            action: () => openBookmark(userItem.action as BookmarkAction),
            actionType: 'Fire and forget',
          }

        console.error('Unexpected action type: ', actionString)

        return null
      })
      .filter((item) => item !== null) as Item[]
  }

  const maxItemsVisible =
    userConfig.maxItemsVisible && userConfig.maxItemsVisible > 0
      ? userConfig.maxItemsVisible
      : defaultConfig.maxItemsVisible

  return {
    excludeApps: userConfig.excludeApps || [],
    favoriteItems: userConfig.favoriteItems || [],
    chromeProfiles,
    items,
    maxItemsVisible,
    theme: {
      color: userConfig.theme?.color || defaultConfig.theme.color,
    },
    debug: userConfig.debug || defaultConfig.debug,
  }
}
