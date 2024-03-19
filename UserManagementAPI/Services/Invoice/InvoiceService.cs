using System;
using User.Management.Service.Models;
using UserManagementAPI.Models;
using UserManagementAPI.Models.Invoice;

namespace UserManagementAPI.Services.Invoice
{
    public class InvoiceService : IInvoice
    {

        private readonly ApplicationDBContext _context;

        public InvoiceService(ApplicationDBContext context)
        {
            _context = context;
        }
        public bool AddInvoice(InvoiceModel invoice)
        {
            try
            {
                _context.Invoice.Add(invoice);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }

        public bool DeleteInvoice(int id)
        {
            try
            {
                var invoice = _context.Invoice.Find(id);
                if (invoice != null)
                {
                    _context.Invoice.Remove(invoice);
                    _context.SaveChanges();
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            return true;
        }

        public bool EditInvoice(InvoiceModel invoice)
        {
            try
            {
                _context.Invoice.Update(invoice);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
                throw;
            }
                return true;
        }

        public InvoiceModel GetInvoice(int id)
        {
            try
            {
                return _context.Invoice.Find(id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<InvoiceModel> GetInvoices()
        {
            try
            {
                return _context.Invoice.ToList();
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
