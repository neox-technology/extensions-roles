namespace Neox.Extensions.Roles.Shared.Tests;

internal sealed class CycleLeft : Role<SampleRoles> {

    public CycleLeft() : base("Left", role => role.DependsOf<CycleRight>()) {
    }
}