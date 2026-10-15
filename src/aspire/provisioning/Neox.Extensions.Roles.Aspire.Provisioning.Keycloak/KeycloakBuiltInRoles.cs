using System.Reflection;
using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

/// <summary>
/// Built-in Keycloak client roles of <c>realm-management</c>.
/// </summary>
public static class KeycloakBuiltInRoles {
    public const string RealmManagementClientId = "realm-management";

    private static readonly HashSet<string> Names = [.. typeof(KeycloakBuiltInRoles).Assembly
        .GetTypes()
        .Where(type => type is { IsClass: true, IsAbstract: false, IsPublic: true }
            && !type.ContainsGenericParameters
            && typeof(IRole).IsAssignableFrom(type))
        .Select(type => ((IRole)Activator.CreateInstance(type)!).Name)];

    public static bool TryGetClientId(string roleName, out string clientId) {
        ArgumentException.ThrowIfNullOrWhiteSpace(roleName);

        if (Names.Contains(roleName)) {
            clientId = RealmManagementClientId;
            return true;
        }

        clientId = string.Empty;
        return false;
    }
}
