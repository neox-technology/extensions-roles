namespace Neox.Extensions.Roles.Shared;

/// <summary>
/// Registers roles that descendants accept without an explicit dependency.
/// </summary>
public interface IImplicitRoleRegistry {

    /// <summary>
    /// Accepts <typeparamref name="TRole"/> and that role's transitive dependents.
    /// </summary>
    void Accept<TRole>()
        where TRole : IRole, new();
}