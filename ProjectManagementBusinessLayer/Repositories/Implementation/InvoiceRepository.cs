using Microsoft.EntityFrameworkCore;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Implementation
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoiceRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void Delete(Invoice invoice)
        {
            _context.Invoices.Remove(invoice);
        }

        public async Task<List<Invoice>> GetAllApprovedInvoices()
        {
            return await _context.Invoices
                .Include(t => t.InvoicePaymentTerms)
                .ThenInclude(r => r.PaymentTerm)
                .Include(e => e.Project)
                .Include(q => q.Project.Client)
                .Where(t=>t.IsApproved==true)
                .Where(r=>r.IsPaidInvoice==false)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetAllInvoices()
        {
            return await _context.Invoices
                .Include(t=>t.InvoicePaymentTerms)
                .ThenInclude(r=>r.PaymentTerm)
                .Include(e=>e.Project)
                .Include(q=>q.Project.Client)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetAllInvoicesByProjectManagerId(string id)
        {
            return await _context.Invoices
                .Include(t => t.InvoicePaymentTerms)
                .ThenInclude(r => r.PaymentTerm)
                .Include(e => e.Project)
                .Include(x=>x.Project.ProjectManager)
                .Include(q => q.Project.Client)
                .Where(n=>n.Project.ProjectManagerId==id)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetAllPaidInvoices()
        {
            return await _context.Invoices
                .Include(t => t.InvoicePaymentTerms)
                .ThenInclude(r => r.PaymentTerm)
                .Include(e => e.Project)
                .Include(q => q.Project.Client)
                .Where(t => t.IsPaidInvoice == true)
                .ToListAsync();
        }

        public async Task<List<Invoice>> GetAllPendingInvoices()
        {
            return await _context.Invoices
                .Include(t => t.InvoicePaymentTerms)
                .ThenInclude(r => r.PaymentTerm)
                .Include(e => e.Project)
                .Include(q => q.Project.Client)
                .Where(t => t.IsApproved == false)
                .ToListAsync();
        }

        public async Task<Invoice> GetInvoiceById(Guid id)
        {
           return await _context.Invoices
                .Include(t => t.InvoicePaymentTerms)
                .ThenInclude(r => r.PaymentTerm)
                .ThenInclude(b=>b.Deliverable)
                .Include(e => e.Project)
                .Include(y=>y.Project.Client)
                .Include(s=>s.Project.ProjectStatus)
                .Include(q=>q.Project.ProjectType)
                .Include(b=>b.Project.ProjectManager)
                
                .SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Invoice> GetInvoiceWithDeliverablesById(Guid id)
        {
            var invoice = await _context.Invoices
            .Include(q => q.Project)
            .Include(r => r.InvoicePaymentTerms)
            .ThenInclude(q => q.PaymentTerm)
            .ThenInclude(v => v.Deliverable)
            .ThenInclude(b=>b.ProjectPhase)
            .SingleOrDefaultAsync(b => b.Id == id);
            return invoice;
        }

        public void Insert(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Invoice invoice)
        {
            _context.Invoices.Update(invoice);
        }
    }
}
