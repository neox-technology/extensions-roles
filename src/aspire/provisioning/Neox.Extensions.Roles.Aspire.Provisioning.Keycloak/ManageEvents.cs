using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ManageEvents : Role<RealmManagementRoles> {

    public ManageEvents() : base("manage-events") {
    }
}
