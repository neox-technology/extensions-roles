using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class QueryOrganizations : Role<RealmManagementRoles> {

    public QueryOrganizations() : base("query-organizations") {
    }
}
