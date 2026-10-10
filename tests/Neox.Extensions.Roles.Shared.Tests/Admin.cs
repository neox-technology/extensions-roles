namespace Neox.Extensions.Roles.Shared.Tests;

public sealed class Admin : Role<Billing> {

    public Admin() : base("Admin", role => role.DependsOf<Writer>()) {
    }
}