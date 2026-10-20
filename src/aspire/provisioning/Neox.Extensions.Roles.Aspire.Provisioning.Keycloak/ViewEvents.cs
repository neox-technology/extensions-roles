using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ViewEvents : Role<RealmManagementRoles> {

    public ViewEvents() : base("view-events") {
    }
}
