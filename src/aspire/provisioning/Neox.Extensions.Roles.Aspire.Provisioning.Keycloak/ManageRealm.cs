using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ManageRealm : Role<RealmManagementRoles> {

    public ManageRealm() : base("manage-realm") {
    }
}
