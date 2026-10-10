namespace Neox.Extensions.Roles.Shared.Tests;

public sealed class SampleRoles : RoleRoot {

    public override void AcceptImplicitly(IImplicitRoleRegistry registry) =>
        registry.Accept<Administrator>();

    public sealed class Administrator : Role<SampleRoles> {

        public Administrator() : base("Administrator") {
        }
    }
}