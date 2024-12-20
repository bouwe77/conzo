import React from 'react'
import { Box, Text } from 'ink'
import { useView } from './ViewContext.js'
import { useConfig } from './config/ConfigContext.js'
import type { BookmarkInfo } from './items/types.js'
import { spawnProcess } from './helpers/spawnProcess.js'
import { SelectBox } from './SelectBox.js'

// Open Google Chrome with the desired profile.

const DEFAULT_PROFILE = 'Default'

export const openChrome = async (profile: string, url?: string) => {
  await spawnProcess('open', [
    '-na',
    'Google Chrome',
    '--args',
    `--profile-directory=${profile}`,
    ...(url ? [url] : []),
  ])
}

export const openBookmark = (bookmark: BookmarkInfo) => {
  openChrome(bookmark.profile ?? DEFAULT_PROFILE, bookmark.url)
}

export const OpenChrome = ({ url }: { url?: string }) => {
  const { goToView } = useView()
  const { chromeProfiles } = useConfig()

  if (!chromeProfiles) {
    return (
      <Box flexDirection="column">
        <Text bold>No Chrome profiles found</Text>
        <Text>
          Make sure you have set up your Chrome profiles in the config.
        </Text>
      </Box>
    )
  }

  return (
    <Box flexDirection="column">
      <Text>Select your account</Text>
      <SelectBox
        items={Object.keys(chromeProfiles)}
        onSelect={(account: string) => {
          if (chromeProfiles[account]) openChrome(chromeProfiles[account], url)
          goToView('search')
        }}
        width={30}
      />
    </Box>
  )
}

export const GoogleChromeWithProfile = ({ url }: { url?: string }) => {
  return <OpenChrome url={url} />
}
