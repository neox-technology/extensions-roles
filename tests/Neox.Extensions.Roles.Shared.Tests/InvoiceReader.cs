namespace Neox.Extensions.Roles.Shared.Tests;

public sealed class InvoiceReader : Role<Invoices> {

    public InvoiceReader() : base("Reader") {
    }
}