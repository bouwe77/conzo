import React from 'react'
import meow from 'meow'
import App from './app.js'
import { ViewProvider } from './ViewContext.js'
import { ApplicationsCacheProvider } from './apps/ApplicationsCacheContext.js'
import type { UserConfig } from './config/types.js'
import { ConfigProvider } from './config/ConfigContext.js'
import { parseAndValidate } from './config/parseAndValidate.js'
import { tryCatch } from './helpers/tryCatch.js'
import { Error, Fallback } from './Error.js'
import { ErrorBoundary } from 'react-error-boundary'
import { render } from 'ink'
import { EscapeBackToSearchProvider } from './EscapeBackToSearchProvider.js'

meow('conzo', {
  importMeta: import.meta,
  flags: {
    name: {
      type: 'string',
    },
  },
})

export const createApp = (userConfig?: UserConfig) => {
  const { value: config, error } = tryCatch(() => parseAndValidate(userConfig))

  const Conzo = config
    ? () => (
        <ErrorBoundary FallbackComponent={Fallback}>
          <ViewProvider>
            <ConfigProvider config={config}>
              <ApplicationsCacheProvider>
                <EscapeBackToSearchProvider>
                  <App />
                </EscapeBackToSearchProvider>
              </ApplicationsCacheProvider>
            </ConfigProvider>
          </ViewProvider>
        </ErrorBoundary>
      )
    : () => <Error error={error} />

  return {
    start: () =>
      render(<Conzo />, {
        exitOnCtrlC: true,
      }),
  }
}
