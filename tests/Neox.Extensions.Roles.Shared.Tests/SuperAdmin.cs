namespace Neox.Extensions.Roles.Shared.Tests;

public sealed class SuperAdmin : Role<Admin> {

    public SuperAdmin() : base("Super") {
    }
}