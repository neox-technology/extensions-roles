using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ViewIdentityProviders : Role<RealmManagementRoles> {

    public ViewIdentityProviders() : base("view-identity-providers") {
    }
}
