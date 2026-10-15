using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class QueryGroups : Role<RealmManagementRoles> {

    public QueryGroups() : base("query-groups") {
    }
}
