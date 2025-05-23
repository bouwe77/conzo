import type {
  BookmarkAction,
  FunctionAction,
  Item,
  ReactAction,
} from '../items/types.js'

export type UserItem = {
  name: string
  action: FunctionAction | ReactAction | BookmarkAction
  alias?: string
}

type FriendlyName = string
type ChromeProfile = string
export type ChromeProfiles = Record<FriendlyName, ChromeProfile> | false

export type Theme = {
  color:
    | 'black'
    | 'red'
    | 'green'
    | 'yellow'
    | 'blue'
    | 'cyan'
    | 'magenta'
    | 'white'
    | 'gray'
    | 'grey'
    | 'blackBright'
    | 'redBright'
    | 'greenBright'
    | 'yellowBright'
    | 'blueBright'
    | 'cyanBright'
    | 'magentaBright'
    | 'whiteBright'
}

export type UserConfig = {
  theme?: Theme
  chromeProfiles?: ChromeProfiles
  excludeApps?: string[]
  favoriteItems?: string[]
  items?: UserItem[]
  maxItemsVisible?: number
  debug?: Debug
}

type Debug = {
  enabled: boolean
}

export type Config = {
  theme: Theme
  debug: Debug
  chromeProfiles: ChromeProfiles
  excludeApps: string[]
  favoriteItems: string[]
  items: Item[]
  maxItemsVisible: number
}
