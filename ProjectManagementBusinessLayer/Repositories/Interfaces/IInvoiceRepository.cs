using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IInvoiceRepository
    {
        public Task<List<Invoice>> GetAllInvoices();
        // return all pending invoices
        public Task<List<Invoice>> GetAllPendingInvoices();
        // return all approved invoices
        public Task<List<Invoice>> GetAllApprovedInvoices();
        // return all invoice based on projectManager id
        public Task<List<Invoice>> GetAllInvoicesByProjectManagerId(string id);
        public Task<Invoice> GetInvoiceById(Guid id);
        public Task<Invoice> GetInvoiceWithDeliverablesById(Guid id);
        public void Insert(Invoice invoice);
        public void Update(Invoice invoice);
        public void Delete(Invoice invoice);
        public void Save();
    }    
}
