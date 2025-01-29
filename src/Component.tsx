import React from 'react'
import type { ReactItem } from './items/types.js'
import { EscapeBackToSearch } from './helpers/EscapeBackToSearch.js'
import { Container } from './Container.js'

type ComponentProps = { item: ReactItem }

export const Component = ({ item }: ComponentProps) => {
  const { action: Component, name } = item

  return (
    <EscapeBackToSearch>
      <Container title={name}>
        <Component />
      </Container>
    </EscapeBackToSearch>
  )
}
