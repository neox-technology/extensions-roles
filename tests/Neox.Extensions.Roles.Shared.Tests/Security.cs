namespace Neox.Extensions.Roles.Shared.Tests;

public sealed class Security : RoleCollection<SampleRoles> {

    public Security() : base("Security") {
    }

    public override void AcceptImplicitly(IImplicitRoleRegistry registry) =>
        registry.Accept<Owner>();

    public sealed class Owner : Role<Security> {

        public Owner() : base("Owner") {
        }
    }

    public sealed class User : Role<Security> {

        public User() : base("User") {
        }
    }
}