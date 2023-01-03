using Microsoft.EntityFrameworkCore;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
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

        public async Task<List<Invoice>> GetAllInvoices()
        {
            return await _context.Invoices.Include(e=>e.Project).ToListAsync();
        }

        public async Task<Invoice> GetInvoiceById(Guid id)
        {
           return await _context.Invoices.Include(e => e.Project).SingleOrDefaultAsync(t => t.Id == id);
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
