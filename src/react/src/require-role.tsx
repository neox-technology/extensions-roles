import type { ReactNode } from "react"

export function RequireRole({
  role,
  roles,
  children,
  fallback = null,
}: {
  role: string
  roles: readonly string[]
  children: ReactNode
  fallback?: ReactNode
}) {
  if (!roles.includes(role)) {
    return fallback
  }

  return children
}
