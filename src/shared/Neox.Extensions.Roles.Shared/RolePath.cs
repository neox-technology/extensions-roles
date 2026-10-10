namespace Neox.Extensions.Roles.Shared;

internal static class RolePath {

    public static string Combine(string parentPath, string localName) {
        if (string.IsNullOrEmpty(parentPath)) {
            return $".{localName}";
        }

        return $"{parentPath}.{localName}";
    }

    public static string Qualify(string parentPath, string localName) {
        var combined = Combine(parentPath, localName);

        return combined.StartsWith('.') ? combined[1..] : combined;
    }
}