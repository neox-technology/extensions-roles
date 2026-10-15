using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ManageUsers : Role<RealmManagementRoles> {

    public ManageUsers() : base("manage-users") {
    }
}
