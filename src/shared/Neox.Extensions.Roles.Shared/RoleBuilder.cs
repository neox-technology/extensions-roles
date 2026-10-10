namespace Neox.Extensions.Roles.Shared;

internal sealed class RoleBuilder : IRoleBuilder {
    private readonly List<string> _dependentRoles = [];

    public IReadOnlyList<string> Build() => _dependentRoles.Distinct(StringComparer.Ordinal).ToArray();

    public IRoleBuilder DependsOf<TRole>()
            where TRole : IRole, new() {
        var role = RoleActivator.Create<TRole>();

        _dependentRoles.Add(role.Name);
        _dependentRoles.AddRange(role.DependentRoles);
        return this;
    }
}