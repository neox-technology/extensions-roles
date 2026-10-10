namespace Neox.Extensions.Roles.Shared;

/// <summary>
/// Leaf role. The name is the parent path plus the local name.
/// </summary>
/// <typeparam name="TRoleParent">Parent collection or role.</typeparam>
public abstract class Role<TRoleParent> : IRole
    where TRoleParent : IRoleParent, new() {
    private static readonly ThreadLocal<HashSet<Type>> Constructing = new(() => []);

    /// <summary>
    /// Creates a role with a local name and optional <see cref="IRoleBuilder.DependsOf{TRole}"/> composition.
    /// </summary>
    protected Role(string name, Action<IRoleBuilder>? role = null) {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        if (!Constructing.Value!.Add(GetType())) {
            throw new InvalidOperationException($"Role construction cycle detected at '{GetType()}'.");
        }

        try {
            Name = RolePath.Qualify(new TRoleParent().GetParentRoleName(), name);

            var builder = new RoleBuilder();

            role?.Invoke(builder);
            DependentRoles = WithImplicitDependents(builder.Build());
        }
        finally {
            Constructing.Value.Remove(GetType());
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<string> DependentRoles { get; }

    /// <inheritdoc />
    public string Name { get; }

    /// <inheritdoc />
    public virtual void AcceptImplicitly(IImplicitRoleRegistry registry) {
    }

    /// <inheritdoc />
    /// <remarks>
    /// Returns the fully-qualified <see cref="Name"/> so a role can parent another role
    /// without duplicating the collection prefix.
    /// </remarks>
    public string GetParentRoleName() => Name;

    private static Type? ClosedGenericBase(Type type, Type openGeneric) {
        for (var current = type; current is not null && current != typeof(object); current = current.BaseType) {
            if (current.IsGenericType && current.GetGenericTypeDefinition() == openGeneric) {
                return current;
            }
        }

        return null;
    }

    private static Type? NextParent(Type type) {
        var role = ClosedGenericBase(type, typeof(Role<>));

        if (role is not null) {
            return role.GetGenericArguments()[0];
        }

        var collection = ClosedGenericBase(type, typeof(RoleCollection<>));

        if (collection is not null) {
            return collection.GetGenericArguments()[0];
        }

        return null;
    }

    private IReadOnlyList<string> WithImplicitDependents(IReadOnlyList<string> dependents) {
        var result = dependents.ToList();
        var type = typeof(TRoleParent);

        while (type is not null && type != typeof(object)) {
            if (type is { IsClass: true, IsAbstract: false } && !type.ContainsGenericParameters) {
                var parent = (IRoleParent)Activator.CreateInstance(type)!;
                var registry = new ImplicitRoleRegistry(GetType());

                parent.AcceptImplicitly(registry);

                foreach (var roleName in registry.RoleNames) {
                    if (!string.Equals(roleName, Name, StringComparison.Ordinal)
                        && !result.Contains(roleName, StringComparer.Ordinal)) {
                        result.Add(roleName);
                    }
                }
            }

            type = NextParent(type);
        }

        return result;
    }
}