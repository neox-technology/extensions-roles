export function RequireRole({ role, roles, children, fallback = null, }) {
    if (!roles.includes(role)) {
        return fallback;
    }
    return children;
}
