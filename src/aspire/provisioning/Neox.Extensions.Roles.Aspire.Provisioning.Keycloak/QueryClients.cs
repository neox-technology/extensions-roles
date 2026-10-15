using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class QueryClients : Role<RealmManagementRoles> {

    public QueryClients() : base("query-clients") {
    }
}
