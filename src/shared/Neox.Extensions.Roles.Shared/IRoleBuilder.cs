namespace Neox.Extensions.Roles.Shared;

/// <summary>
/// Composes accepted roles onto the role being constructed.
/// </summary>
public interface IRoleBuilder {

    /// <summary>
    /// Accepts <typeparamref name="TRole"/> and that role's transitive dependents.
    /// </summary>
    IRoleBuilder DependsOf<TRole>()
        where TRole : IRole, new();
}