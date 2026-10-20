export function combine(parentPath: string, localName: string): string {
  if (parentPath === "") {
    return `.${localName}`
  }

  return `${parentPath}.${localName}`
}

export function qualify(parentPath: string, localName: string): string {
  const combined = combine(parentPath, localName)

  return combined.startsWith(".") ? combined.slice(1) : combined
}
