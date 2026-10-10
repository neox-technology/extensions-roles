using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Neox.Extensions.Roles.Shared;

namespace Neox.Aspire.Hosting.Keycloak;

/// <summary>
/// Declares catalog roles as realm roles for a Keycloak client.
/// </summary>
public static class KeycloakRoleCollectionExtensions {

    /// <summary>
    /// Upserts realm roles for <typeparamref name="TCollection"/> on the parent realm.
    /// </summary>
    public static IResourceBuilder<KeycloakJwtClientResource> WithRoles<TCollection>(
        this IResourceBuilder<KeycloakJwtClientResource> builder)
        where TCollection : IRoleCollection, new() {
        ArgumentNullException.ThrowIfNull(builder);

        Apply<TCollection>(builder.ApplicationBuilder, builder.Resource.Parent);

        return builder;
    }

    /// <summary>
    /// Upserts realm roles for <typeparamref name="TCollection"/> on the parent realm.
    /// </summary>
    public static IResourceBuilder<KeycloakOidcClientResource> WithRoles<TCollection>(
        this IResourceBuilder<KeycloakOidcClientResource> builder)
        where TCollection : IRoleCollection, new() {
        ArgumentNullException.ThrowIfNull(builder);

        Apply<TCollection>(builder.ApplicationBuilder, builder.Resource.Parent);

        return builder;
    }

    private static void Apply<TCollection>(
        IDistributedApplicationBuilder application,
        KeycloakRealmResource realm)
        where TCollection : IRoleCollection, new() {
        var realmBuilder = application.CreateResourceBuilder(realm);

        foreach (var composition in RoleCatalog.Under<TCollection>()) {
            realmBuilder.WithRealmRole(composition.Name);

            foreach (var dependent in composition.DependentRoles) {
                realmBuilder.WithRealmRole(dependent);
                realmBuilder.WithRealmRoleInheritance(dependent, [composition.Name]);
            }
        }
    }
}