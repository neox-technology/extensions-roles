using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ManageIdentityProviders : Role<RealmManagementRoles> {

    public ManageIdentityProviders() : base("manage-identity-providers") {
    }
}
