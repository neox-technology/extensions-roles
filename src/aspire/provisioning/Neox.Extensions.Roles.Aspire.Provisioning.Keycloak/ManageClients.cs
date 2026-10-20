using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ManageClients : Role<RealmManagementRoles> {

    public ManageClients() : base("manage-clients") {
    }
}
