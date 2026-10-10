namespace Neox.Extensions.Roles.Shared.Tests;

public sealed class CycleTests {

    [Fact]
    public void DependsOf_cycle_throws() {
        var error = Assert.Throws<InvalidOperationException>(() => new CycleLeft());

        Assert.Contains(nameof(CycleLeft), error.Message, StringComparison.Ordinal);
    }
}