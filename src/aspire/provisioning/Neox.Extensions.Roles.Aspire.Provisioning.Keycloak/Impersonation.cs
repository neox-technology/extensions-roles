using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class Impersonation : Role<RealmManagementRoles> {

    public Impersonation() : base("impersonation") {
    }
}
