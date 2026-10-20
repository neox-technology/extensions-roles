export function combine(parentPath, localName) {
    if (parentPath === "") {
        return `.${localName}`;
    }
    return `${parentPath}.${localName}`;
}
export function qualify(parentPath, localName) {
    const combined = combine(parentPath, localName);
    return combined.startsWith(".") ? combined.slice(1) : combined;
}
