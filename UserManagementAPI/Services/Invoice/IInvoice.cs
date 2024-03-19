using UserManagementAPI.Models.Invoice;

namespace UserManagementAPI.Services.Invoice
{
    public interface IInvoice
    {
        List<InvoiceModel> GetInvoices();
        InvoiceModel GetInvoice(int id);
        bool EditInvoice(InvoiceModel invoice);
        bool DeleteInvoice(int id);
        bool AddInvoice(InvoiceModel invoice);
    }
}
