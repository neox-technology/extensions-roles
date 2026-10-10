using System.Reflection;

namespace Neox.Extensions.Roles.Shared;

/// <summary>
/// Discovers fully-qualified role names from catalog assemblies.
/// </summary>
public static class RoleCatalog {

    public static IReadOnlyList<string> FromAssemblies(params Assembly[] assemblies) {
        ArgumentNullException.ThrowIfNull(assemblies);

        return [.. Roles(assemblies)
            .Select(role => role.Name)
            .Distinct(StringComparer.Ordinal)
            .OrderBy(name => name, StringComparer.Ordinal)];
    }

    /// <summary>
    /// Compositions whose names sit under <typeparamref name="TCollection"/>.
    /// When <paramref name="assemblies"/> is empty, scans the collection's assembly.
    /// </summary>
    public static IReadOnlyList<RoleComposition> Under<TCollection>(params Assembly[] assemblies)
        where TCollection : IRoleCollection, new() {
        ArgumentNullException.ThrowIfNull(assemblies);

        var prefix = new TCollection().GetParentRoleName().TrimStart('.') + ".";
        var source = assemblies.Length == 0 ? [typeof(TCollection).Assembly] : assemblies;

        return [.. Roles(source)
            .Where(role => role.Name.StartsWith(prefix, StringComparison.Ordinal))
            .Select(role => new RoleComposition(role.Name, role.DependentRoles))
            .DistinctBy(role => role.Name)
            .OrderBy(role => role.Name, StringComparer.Ordinal)];
    }

    private static IEnumerable<IRole> Roles(IEnumerable<Assembly> assemblies) {
        return assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type is { IsClass: true, IsAbstract: false }
                && (type.IsPublic || type.IsNestedPublic)
                && !type.ContainsGenericParameters
                && typeof(IRole).IsAssignableFrom(type))
            .Select(type => (IRole)Activator.CreateInstance(type)!);
    }
}