namespace Neox.Extensions.Roles.Shared.Tests;

public sealed class RoleCatalogTests {

    [Fact]
    public void All_lists_public_roles() {
        var roles = RoleCatalog.All(typeof(SampleRoles).Assembly);

        Assert.Equal(
            [
                "Administrator",
                "Billing.Admin",
                "Billing.Admin.Super",
                "Billing.Invoices.Reader",
                "Billing.Reader",
                "Billing.Writer",
                "Security.Owner",
                "Security.User",
            ],
            roles.Select(role => role.Name));
    }

    [Fact]
    public void FromAssemblies_lists_public_role_names() {
        Assert.Equal(
            [
                "Administrator",
                "Billing.Admin",
                "Billing.Admin.Super",
                "Billing.Invoices.Reader",
                "Billing.Reader",
                "Billing.Writer",
                "Security.Owner",
                "Security.User",
            ],
            RoleCatalog.FromAssemblies(typeof(SampleRoles).Assembly));
    }

    [Fact]
    public void Under_Billing_is_the_billing_roles() {
        var compositions = RoleCatalog.Under<Billing>();

        Assert.Equal(
            [
                "Billing.Admin",
                "Billing.Admin.Super",
                "Billing.Invoices.Reader",
                "Billing.Reader",
                "Billing.Writer",
            ],
            compositions.Select(role => role.Name));
    }

    [Fact]
    public void Under_Security_includes_the_implicit_owner() {
        var compositions = RoleCatalog.Under<Security>();

        Assert.Equal(["Security.Owner", "Security.User"], compositions.Select(role => role.Name));
        Assert.Equal(["Administrator"], compositions[0].DependentRoles);
        Assert.Equal(["Security.Owner", "Administrator"], compositions[1].DependentRoles);
    }
}