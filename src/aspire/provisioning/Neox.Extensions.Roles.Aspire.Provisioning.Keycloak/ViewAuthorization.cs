using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class ViewAuthorization : Role<RealmManagementRoles> {

    public ViewAuthorization() : base("view-authorization") {
    }
}
