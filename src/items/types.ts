type BaseItem<T extends string> = {
  name: string
  actionType: T
  alias?: string
}

// Functions
export type FunctionAction = () => Promise<void>
export type FunctionItem = BaseItem<'Fire and forget'> & {
  action: FunctionAction
}

// UI
export type ReactAction = () => JSX.Element
export type ReactItem = BaseItem<'Show UI'> & {
  action: ReactAction
}

// Bookmarks
export type BookmarkInfo = {
  browser?: string
  profile?: string
  url: string
}
export type BookmarkAction = BookmarkInfo
export type BookmarkItem = BaseItem<'Open URL'> & {
  action: BookmarkAction
}

export type Item = FunctionItem | ReactItem | BookmarkItem
