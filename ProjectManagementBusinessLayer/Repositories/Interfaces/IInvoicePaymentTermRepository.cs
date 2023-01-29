using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IInvoicePaymentTermsRepository
    {
        // get all InvoicePayments
        public Task<List<InvoicePaymentTerms>> GetAllInvoicesPaymetnTerms();
        // get all InvoicePayment by id
        public Task<InvoicePaymentTerms> GetInvoicePaymentTermById(Guid id);
        // get all InvoicePayment by projectmanagerid
        public Task<List<InvoicePaymentTerms>> GetInvoicePaymentTermByIdByProjectId(Guid id);
        // get all InvoicePayment by invoiceidid
        public Task<List<InvoicePaymentTerms>> GetInvoicePaymentTermByIdByInvoiceId(Guid id);
        public Task<List<InvoicePaymentTerms>> FindAllByCondition(Expression<Func<InvoicePaymentTerms, bool>> predicate);
        public Task<InvoicePaymentTerms> FindConditionById(Expression<Func<InvoicePaymentTerms, bool>> predicate);
        public void Insert(InvoicePaymentTerms invoicePayment);
        public void Update(InvoicePaymentTerms invoicePayment);
        public void Delete(InvoicePaymentTerms invoicePayment);
        public void Save();
    }
}
