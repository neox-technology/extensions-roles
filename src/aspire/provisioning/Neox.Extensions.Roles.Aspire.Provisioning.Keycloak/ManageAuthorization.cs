using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ManageAuthorization : Role<RealmManagementRoles> {

    public ManageAuthorization() : base("manage-authorization") {
    }
}
