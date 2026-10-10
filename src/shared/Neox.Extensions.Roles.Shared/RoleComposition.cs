namespace Neox.Extensions.Roles.Shared;

/// <summary>
/// A role name and the role names it also accepts, ready to push to an identity provider.
/// </summary>
public sealed record RoleComposition(string Name, IReadOnlyList<string> DependentRoles) {
    /// <summary>
    /// Builds a composition from a catalog role type.
    /// </summary>
    public static RoleComposition From<TRole>()
        where TRole : IRole, new() {
        var role = new TRole();

        return new(role.Name, role.DependentRoles);
    }
}