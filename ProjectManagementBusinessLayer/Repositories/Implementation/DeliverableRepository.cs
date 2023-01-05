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
    public class DeliverableRepository : IDeliverableRepository
    {

        private readonly ApplicationDbContext _context;

        public DeliverableRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Delete(Deliverable deliverable)
        {
            _context.Deliverables.Remove(deliverable);
        }

        public async Task<List<Deliverable>> GetAllDeliverableForProjectManager(string id)
        {
            return await _context.Deliverables
                .Include(v => v.ProjectPhase)
                .ThenInclude(x => x.Project)
                .ThenInclude(e => e.ProjectPhases)
                .ThenInclude(q => q.Phase)
                .Where(n => n.ProjectPhase.Project.ProjectManagerId == id)
                .ToListAsync();
        }

        public async Task<List<Deliverable>> GetAllDeliverables()
        {
            return await _context.Deliverables
                .Include(v => v.ProjectPhase)
                .ThenInclude(x => x.Project)
                .ThenInclude(e => e.ProjectPhases)
                .ThenInclude(q => q.Phase)
                .ToListAsync();
        }

        public async Task<Deliverable> GetDeliverableById(Guid id)
        {
            return await _context.Deliverables
                .Include(v => v.ProjectPhase)
                .ThenInclude(x => x.Project)
                .ThenInclude(e => e.ProjectPhases)
                .ThenInclude(q => q.Phase)
                .SingleOrDefaultAsync(g => g.Id == id);
        }

        public void Insert(Deliverable deliverable)
        {
            _context.Deliverables.Add(deliverable);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Deliverable deliverable)
        {
            _context.Deliverables.Update(deliverable);
        }
    }
}
