using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ViewOrganizations : Role<RealmManagementRoles> {

    public ViewOrganizations() : base("view-organizations") {
    }
}
