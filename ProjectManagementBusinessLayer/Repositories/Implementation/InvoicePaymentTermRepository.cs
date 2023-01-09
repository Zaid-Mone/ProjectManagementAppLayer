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
    public class InvoicePaymentTermsRepository : IInvoicePaymentTermsRepository
    {
        private readonly ApplicationDbContext _context;

        public InvoicePaymentTermsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(InvoicePaymentTerms invoicePayment)
        {
            _context.InvoicePaymentTerms.Remove(invoicePayment);
        }

        public async Task<List<InvoicePaymentTerms>> GetAllInvoicesPaymetnTerms()
        {
            return await _context.InvoicePaymentTerms
                .Include(q => q.Invoice)
                .Include(w => w.PaymentTerm)
                .Include(e => e.PaymentTerm.Deliverable)
                .Include(r => r.PaymentTerm.Deliverable.ProjectPhase)
                .Include(y => y.PaymentTerm.Deliverable.ProjectPhase.Phase)
                .Include(t => t.PaymentTerm.Deliverable.ProjectPhase.Project)
                .Include(o => o.PaymentTerm.Deliverable.ProjectPhase.Project.Client)
                .Include(f => f.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectType)
                .Include(g => g.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectStatus)
                .Include(u => u.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectManager)
                .ToListAsync();
        }

        public async Task<InvoicePaymentTerms> GetInvoicePaymentTermById(Guid id)
        {
            return await _context.InvoicePaymentTerms
                    .Include(q => q.Invoice)
                    .Include(w => w.PaymentTerm)
                    .Include(e => e.PaymentTerm.Deliverable)
                    .Include(r => r.PaymentTerm.Deliverable.ProjectPhase)
                    .Include(y => y.PaymentTerm.Deliverable.ProjectPhase.Phase)
                    .Include(t => t.PaymentTerm.Deliverable.ProjectPhase.Project)
                    .Include(o => o.PaymentTerm.Deliverable.ProjectPhase.Project.Client)
                    .Include(f => f.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectType)
                    .Include(g => g.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectStatus)
                    .Include(u => u.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectManager)
                    .SingleOrDefaultAsync(b => b.InvoiceId == id && b.PaymentTermId == id);
        }

        public async Task<List<InvoicePaymentTerms>> GetInvoicePaymentTermByIdByInvoiceId(Guid id)
        {
            return await _context.InvoicePaymentTerms
                .Include(q => q.Invoice)
                .Include(w => w.PaymentTerm)
                .Include(e => e.PaymentTerm.Deliverable)
                .Include(r => r.PaymentTerm.Deliverable.ProjectPhase)
                .Include(y => y.PaymentTerm.Deliverable.ProjectPhase.Phase)
                .Include(t => t.PaymentTerm.Deliverable.ProjectPhase.Project)
                .Include(o => o.PaymentTerm.Deliverable.ProjectPhase.Project.Client)
                .Include(f => f.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectType)
                .Include(g => g.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectStatus)
                .Include(u => u.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectManager)
                .Where(q => q.InvoiceId == id)
                .ToListAsync();
        }

        public async Task<List<InvoicePaymentTerms>> GetInvoicePaymentTermByIdByProjectId(Guid id)
        {
            return await _context.InvoicePaymentTerms
                .Include(q => q.Invoice)
                .Include(w => w.PaymentTerm)
                .Include(e => e.PaymentTerm.Deliverable)
                .Include(r => r.PaymentTerm.Deliverable.ProjectPhase)
                .Include(y => y.PaymentTerm.Deliverable.ProjectPhase.Phase)
                .Include(t => t.PaymentTerm.Deliverable.ProjectPhase.Project)
                .Include(o => o.PaymentTerm.Deliverable.ProjectPhase.Project.Client)
                .Include(f => f.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectType)
                .Include(g => g.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectStatus)
                .Include(u => u.PaymentTerm.Deliverable.ProjectPhase.Project.ProjectManager)
                .Where(q=>q.InvoiceId==id)
                .ToListAsync();
        }

        public void Insert(InvoicePaymentTerms invoicePayment)
        {
            _context.InvoicePaymentTerms.Add(invoicePayment);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(InvoicePaymentTerms invoicePayment)
        {
            _context.InvoicePaymentTerms.Update(invoicePayment);
        }
    }
}
