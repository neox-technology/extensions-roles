using Neox.Extensions.Roles.Shared;

namespace Neox.Extensions.Roles.Provisioning.Keycloak;

public sealed class CreateClient : Role<RealmManagementRoles> {

    public CreateClient() : base("create-client") {
    }
}
