import React from 'react'
import { ReactItem } from './items/types.js'
import { EscapeBackToSearch } from './helpers/EscapeBackToSearch.js'

type ComponentProps = { item: ReactItem }

export const Component = ({ item }: ComponentProps) => {
  const { action: Component, name } = item

  return (
    <EscapeBackToSearch title={name}>
      <Component />
    </EscapeBackToSearch>
  )
}
