using Aspire.Hosting.ApplicationModel;
using Neox.Extensions.Roles.Shared;

namespace Neox.Aspire.Hosting.Azure;

/// <summary>
/// Declares catalog roles as Entra app roles on an app registration.
/// </summary>
public static class AzureEntraIdRoleCollectionExtensions {

    /// <summary>
    /// Adds one Graph app role per role under <typeparamref name="TCollection"/>,
    /// including dependent role names.
    /// </summary>
    public static IResourceBuilder<AzureEntraIdAppRegistrationResource> WithRoles<TCollection>(
        this IResourceBuilder<AzureEntraIdAppRegistrationResource> builder,
        AllowedMemberTypes allowedMemberTypes = AllowedMemberTypes.User)
        where TCollection : IRoleCollection, new() {
        ArgumentNullException.ThrowIfNull(builder);

        foreach (var roleName in RoleNames<TCollection>()) {
            try {
                builder.AddAppRole(
                    $"{builder.Resource.Name}-role-{ToKebab(roleName)}",
                    allowedMemberTypes,
                    roleName,
                    roleName,
                    roleName);
            }
            catch (ArgumentException exception) when (string.Equals(exception.ParamName, "value", StringComparison.Ordinal)) {
                // AddAppRole rejects a duplicate value. AppRoles is internal on this package version.
            }
        }

        return builder;
    }

    private static IEnumerable<string> RoleNames<TCollection>()
        where TCollection : IRoleCollection, new() {
        var compositions = RoleCatalog.Under<TCollection>();

        return compositions
            .Select(role => role.Name)
            .Concat(compositions.SelectMany(role => role.DependentRoles))
            .Distinct(StringComparer.Ordinal)
            .OrderBy(name => name, StringComparer.Ordinal);
    }

    private static string ToKebab(string roleName) =>
        roleName.Replace('.', '-').ToLowerInvariant();
}