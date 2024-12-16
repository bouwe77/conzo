import { Config, UserConfig, UserItem } from './types.js'
import { BookmarkAction, Item } from '../items/types.js'
import { openBookmark } from '../GoogleChrome.js'

const defaultConfig: Config = {
  excludeApps: [],
  favoriteItems: [],
  chromeProfiles: false,
  items: [],
  theme: {
    color: 'green',
  },
}

export const parseAndValidate = (userConfig?: UserConfig): Config => {
  if (!userConfig) return defaultConfig

  const chromeProfiles = userConfig.chromeProfiles || false

  const items = userConfig.items
    ?.map((userItem: UserItem) => {
      const actionString = userItem.action.toString()
      // A function that returns JSX is considered a UI action
      if (actionString.startsWith('() => React.createElement('))
        return {
          name: userItem.name,
          action: userItem.action as () => JSX.Element,
          actionType: 'Show UI',
        }

      // A function that returns a promise is considered a fire-and-forget action
      if (actionString.startsWith('() => Promise.resolve('))
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

      return null
    })
    .filter((item) => item !== null) as Item[]

  return {
    excludeApps: userConfig.excludeApps || [],
    favoriteItems: userConfig.favoriteItems || [],
    chromeProfiles,
    items,
    theme: {
      color: userConfig.theme?.color || defaultConfig.theme.color,
    },
  }
}
