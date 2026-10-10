namespace Neox.Extensions.Roles.Shared.Tests;

internal sealed class CycleRight : Role<SampleRoles> {

    public CycleRight() : base("Right", role => role.DependsOf<CycleLeft>()) {
    }
}