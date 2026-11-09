using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Neox.Extensions.Roles.Shared;

namespace Neox.Aspire.Hosting.Azure;

/// <summary>
/// Declares catalog roles as Entra app roles on an app registration.
/// </summary>
public static class AzureEntraIdRoleCollectionExtensions {

    /// <summary>
    /// Requests the catalog app role <typeparamref name="TRole"/> as a Graph permission
    /// on this app registration. The role value must be unique in the application model.
    /// </summary>
    public static IResourceBuilder<AzureEntraIdAppRegistrationResource> WithPermission<TRole>(
        this IResourceBuilder<AzureEntraIdAppRegistrationResource> builder)
        where TRole : IRole, new() {
        ArgumentNullException.ThrowIfNull(builder);

        var roleName = new TRole().Name;
        var role = builder.ApplicationBuilder.Resources
            .OfType<AzureEntraIdAppRoleResource>()
            .Single(candidate => string.Equals(candidate.Value, roleName, StringComparison.Ordinal));

        return builder.WithPermission(builder.ApplicationBuilder.CreateResourceBuilder(role));
    }

    /// <summary>
    /// Adds one Graph app role per role under <typeparamref name="TCollection"/>,
    /// including dependent role names.
    /// </summary>
    public static IResourceBuilder<AzureEntraIdAppRegistrationResource> WithRoleCatalog<TCollection>(
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
