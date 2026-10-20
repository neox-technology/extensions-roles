using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ManageOrganizations : Role<RealmManagementRoles> {

    public ManageOrganizations() : base("manage-organizations") {
    }
}
