namespace Neox.Extensions.Roles.Shared;

/// <summary>
/// Named collection whose dotted path is the parent collection path plus <see cref="Name"/>.
/// </summary>
/// <typeparam name="TRoleCollection">Parent collection type.</typeparam>
public abstract class RoleCollection<TRoleCollection> : IRoleCollection
    where TRoleCollection : IRoleCollection, new() {

    /// <summary>
    /// Creates a collection with the given local segment name.
    /// </summary>
    protected RoleCollection(string name) {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        Name = name;
    }

    /// <summary>
    /// Local collection segment (for example <c>Billing</c>).
    /// </summary>
    public string Name { get; }

    /// <inheritdoc />
    public virtual void AcceptImplicitly(IImplicitRoleRegistry registry) {
    }

    /// <inheritdoc />
    public string GetParentRoleName() =>
        RolePath.Combine(new TRoleCollection().GetParentRoleName(), Name);
}