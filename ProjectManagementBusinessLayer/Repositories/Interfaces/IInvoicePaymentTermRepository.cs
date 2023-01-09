using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IInvoicePaymentTermsRepository
    {
        public Task<List<InvoicePaymentTerms>> GetAllInvoicesPaymetnTerms();
        public Task<InvoicePaymentTerms> GetInvoicePaymentTermById(Guid id);
        public Task<List<InvoicePaymentTerms>> GetInvoicePaymentTermByIdByProjectId(Guid id);
        public void Insert(InvoicePaymentTerms invoicePayment);
        public void Update(InvoicePaymentTerms invoicePayment);
        public void Delete(InvoicePaymentTerms invoicePayment);
        public void Save();
    }
}
