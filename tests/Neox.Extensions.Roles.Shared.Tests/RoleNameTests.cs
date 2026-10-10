namespace Neox.Extensions.Roles.Shared.Tests;

public sealed class RoleNameTests {

    [Fact]
    public void Collection_parent_path_includes_leading_dot_for_root_child() {
        Assert.Equal(".Billing", new Billing().GetParentRoleName());
    }

    [Fact]
    public void Nested_collection_qualifies_role_name() {
        Assert.Equal("Billing.Invoices.Reader", new InvoiceReader().Name);
    }

    [Fact]
    public void Reader_name_is_collection_plus_local_name() {
        Assert.Equal("Billing.Reader", new Reader().Name);
    }

    [Fact]
    public void Role_parent_uses_fully_qualified_name_without_double_prefix() {
        Assert.Equal("Billing.Admin.Super", new SuperAdmin().Name);
    }
}