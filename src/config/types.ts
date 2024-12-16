import {
  BookmarkAction,
  FunctionAction,
  Item,
  ReactAction,
} from '../items/types.js'

export type UserItem = {
  name: string
  action: FunctionAction | ReactAction | BookmarkAction
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
}

export type Config = {
  theme: Theme
  chromeProfiles: ChromeProfiles
  excludeApps: string[]
  favoriteItems: string[]
  items: Item[]
}
