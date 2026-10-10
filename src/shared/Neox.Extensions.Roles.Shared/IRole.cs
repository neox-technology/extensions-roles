namespace Neox.Extensions.Roles.Shared;

/// <summary>
/// A leaf role with a fully-qualified name and optional dependents.
/// </summary>
public interface IRole : IRoleParent {

    /// <summary>
    /// Role names this role also accepts, including transitive <see cref="IRoleBuilder.DependsOf{TRole}"/>.
    /// </summary>
    IReadOnlyList<string> DependentRoles { get; }

    /// <summary>
    /// Fully-qualified dotted role name (for example <c>Billing.Admin</c>).
    /// </summary>
    string Name { get; }
}