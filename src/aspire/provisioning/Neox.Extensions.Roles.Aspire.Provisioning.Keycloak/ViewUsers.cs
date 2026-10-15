using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ViewUsers : Role<RealmManagementRoles> {

    public ViewUsers() : base("view-users") {
    }
}
