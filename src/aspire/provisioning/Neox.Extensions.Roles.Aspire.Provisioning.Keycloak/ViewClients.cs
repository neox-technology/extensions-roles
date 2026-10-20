using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ViewClients : Role<RealmManagementRoles> {

    public ViewClients() : base("view-clients") {
    }
}
