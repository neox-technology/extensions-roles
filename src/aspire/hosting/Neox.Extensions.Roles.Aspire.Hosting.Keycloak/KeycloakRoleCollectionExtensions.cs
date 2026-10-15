using System.Collections.Concurrent;
using System.Text.Json;
using Aspire.Hosting;
using Aspire.Hosting.ApplicationModel;
using Neox.Extensions.Roles.Provisioning.Keycloak;
using Neox.Extensions.Roles.Shared;
using Neox.Keycloak.Provisioning.Realm;

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
        var catalog = RoleCatalog.All(typeof(TCollection).Assembly)
            .ToDictionary(role => role.Name, StringComparer.Ordinal);
        var realmBuilder = application.CreateResourceBuilder(realm);
        var visited = new HashSet<string>(StringComparer.Ordinal);

        foreach (var composition in RoleCatalog.Under<TCollection>()) {
            realmBuilder.WithRealmRole(composition.Name);

            foreach (var dependent in composition.DependentRoles) {
                Attach(realmBuilder, composition.Name, dependent, catalog, visited);
            }
        }
    }

    private static void Attach(
        IResourceBuilder<KeycloakRealmResource> realmBuilder,
        string parentName,
        string childName,
        IReadOnlyDictionary<string, IRole> catalog,
        HashSet<string> visited) {
        if (!visited.Add($"{parentName}\0{childName}")) {
            return;
        }

        if (KeycloakBuiltInRoles.TryGetClientId(childName, out var clientId)) {
            IncludeClientRole(realmBuilder, parentName, clientId, childName);
            return;
        }

        realmBuilder.WithRealmRole(childName);
        realmBuilder.WithRealmRoleInheritance(childName, [parentName]);

        if (catalog.TryGetValue(childName, out var child)) {
            foreach (var grand in child.DependentRoles) {
                Attach(realmBuilder, childName, grand, catalog, visited);
            }
        }
    }

    private static void IncludeClientRole(
        IResourceBuilder<KeycloakRealmResource> realm,
        string parentRoleName,
        string clientId,
        string clientRoleName) {
        var realmFilePath = Path.Combine(realm.Resource.ImportDirectory, $"{realm.Resource.Realm}-realm.json");
        var json = File.ReadAllText(realmFilePath);
        var representation = JsonSerializer.Deserialize<RealmRepresentation>(json, KeycloakRealmJsonOptions.Default)
            ?? throw new InvalidOperationException($"Failed to deserialize realm JSON at '{realmFilePath}'.");

        representation.Roles ??= new RolesRepresentation();
        representation.Roles.Realm ??= [];

        var parent = representation.Roles.Realm.GetOrAdd(
            role => role.Name == parentRoleName,
            new RoleRepresentation { Name = parentRoleName });

        parent.Composite = true;
        parent.Composites ??= new Composites();
        parent.Composites.Client ??= new ConcurrentDictionary<string, ConcurrentBag<string>>(StringComparer.Ordinal);

        var names = parent.Composites.Client.GetOrAdd(clientId, _ => []);

        names.GetOrAdd(existing => existing == clientRoleName, clientRoleName);

        File.WriteAllText(
            realmFilePath,
            JsonSerializer.Serialize(representation, KeycloakRealmJsonOptions.Default));
    }
}
