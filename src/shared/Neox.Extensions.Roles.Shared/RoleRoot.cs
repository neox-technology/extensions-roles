namespace Neox.Extensions.Roles.Shared;

/// <summary>
/// Composition root. Parent path is empty.
/// </summary>
public abstract class RoleRoot : IRoleCollection {

    /// <inheritdoc />
    public virtual void AcceptImplicitly(IImplicitRoleRegistry registry) {
    }

    /// <inheritdoc />
    public string GetParentRoleName() => string.Empty;
}