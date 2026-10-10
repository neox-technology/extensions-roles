namespace Neox.Extensions.Roles.Shared.Tests;

public sealed class Writer : Role<Billing> {

    public Writer() : base("Writer", role => role.DependsOf<Reader>()) {
    }
}