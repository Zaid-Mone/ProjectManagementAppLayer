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
    public class PaymentTermRepository : IPaymentTermRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentTermRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        
        public void Delete(PaymentTerm paymentTerm)
        {
            _context.PaymentTerms.Remove(paymentTerm);
        }

        public async Task<List<PaymentTerm>> GetAllPaymentTerms()
        {
            return await _context.PaymentTerms
                .Include(e => e.Deliverable)
                .Include(q => q.Deliverable.ProjectPhase)
                .Include(y => y.Deliverable.ProjectPhase.Project)
                .Include(v => v.Deliverable.ProjectPhase.Phase)
                .ToListAsync();
        }

        public async Task<PaymentTerm> GetPaymentTermById(Guid id)
        {
            return await _context.PaymentTerms
                .Include(e => e.Deliverable)
                .Include(q => q.Deliverable.ProjectPhase)
                .Include(y => y.Deliverable.ProjectPhase.Project)
                .Include(v => v.Deliverable.ProjectPhase.Phase)
                .SingleOrDefaultAsync(i => i.Id == id);
        }

        public List<PaymentTerm> GetProjectPaymentTerms(Guid id)
        {
            return _context.PaymentTerms
                .Include(e => e.Deliverable)
                .Include(q => q.Deliverable.ProjectPhase)
                .Include(y => y.Deliverable.ProjectPhase.Project)
                .Include(v => v.Deliverable.ProjectPhase.Phase)
           .Where(r => r.Deliverable.ProjectPhase.Project.Id == id)
           .ToList();
        }

        public void Insert(PaymentTerm paymentTerm)
        {
            _context.PaymentTerms.Add(paymentTerm);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(PaymentTerm paymentTerm)
        {
            _context.PaymentTerms.Update(paymentTerm);
        }
    }
}
