type Result<T> = { value: T; error: null } | { value: null; error: Error }

export function tryCatch<T>(fn: () => T): Result<T> {
  try {
    return { value: fn(), error: null }
  } catch (error) {
    return {
      value: null,
      error: error instanceof Error ? error : new Error(String(error)),
    }
  }
}
