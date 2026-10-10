namespace Neox.Extensions.Roles.Shared.Tests;

public sealed class ImplicitRoleTests {

    [Fact]
    public void Admin_depends_of_writer_transitively_includes_reader_and_root() {
        Assert.Equal(
            ["Billing.Writer", "Billing.Reader", "Administrator"],
            new Admin().DependentRoles);
    }

    [Fact]
    public void Administrator_does_not_accept_itself() {
        Assert.Empty(new SampleRoles.Administrator().DependentRoles);
    }

    [Fact]
    public void Collection_implicit_role_is_accepted_by_descendants() {
        Assert.Equal(
            ["Security.Owner", "Administrator"],
            new Security.User().DependentRoles);
    }

    [Fact]
    public void Reader_accepts_root_administrator_without_depends_of() {
        Assert.Equal(["Administrator"], new Reader().DependentRoles);
    }

    [Fact]
    public void Security_owner_accepts_root_and_not_itself() {
        Assert.Equal(["Administrator"], new Security.Owner().DependentRoles);
    }

    [Fact]
    public void Writer_depends_of_reader_and_root() {
        Assert.Equal(["Billing.Reader", "Administrator"], new Writer().DependentRoles);
    }
}