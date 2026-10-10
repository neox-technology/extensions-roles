namespace Neox.Extensions.Roles.Shared;

/// <summary>
/// A node in the role type tree that contributes a dotted parent path.
/// </summary>
public interface IRoleParent {

    /// <summary>
    /// Registers roles that descendants accept without an explicit <see cref="IRoleBuilder.DependsOf{TRole}"/>.
    /// </summary>
    void AcceptImplicitly(IImplicitRoleRegistry registry) {
    }

    /// <summary>
    /// Returns the dotted parent path used to qualify child names.
    /// The root collection returns an empty string.
    /// </summary>
    string GetParentRoleName();
}