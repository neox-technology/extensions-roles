using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class QueryUsers : Role<RealmManagementRoles> {

    public QueryUsers() : base("query-users") {
    }
}
