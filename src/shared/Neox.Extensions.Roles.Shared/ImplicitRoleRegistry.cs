namespace Neox.Extensions.Roles.Shared;

internal sealed class ImplicitRoleRegistry : IImplicitRoleRegistry {
    private readonly List<string> _roleNames = [];
    private readonly Type _self;

    public ImplicitRoleRegistry(Type self) {
        ArgumentNullException.ThrowIfNull(self);
        _self = self;
    }

    public IReadOnlyList<string> RoleNames => _roleNames;

    public void Accept<TRole>()
            where TRole : IRole, new() {
        if (typeof(TRole) == _self) {
            return;
        }

        var role = RoleActivator.Create<TRole>();

        Add(role.Name);

        foreach (var dependent in role.DependentRoles) {
            Add(dependent);
        }
    }

    private void Add(string name) {
        if (!_roleNames.Contains(name, StringComparer.Ordinal)) {
            _roleNames.Add(name);
        }
    }
}