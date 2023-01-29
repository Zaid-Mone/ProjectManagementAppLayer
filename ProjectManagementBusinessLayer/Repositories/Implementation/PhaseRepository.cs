using Microsoft.EntityFrameworkCore;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Implementation
{
    public class PhaseRepository : IPhaseRepository
    {
        private readonly ApplicationDbContext _context;

        public PhaseRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Delete(Phase phase)
        {
            _context.Phases.Remove(phase);
        }

        public async Task<List<Phase>> FindAllByCondition(Expression<Func<Phase, bool>> predicate)
        {
            return await _context.Phases.Where(predicate).ToListAsync();
        }

        public async Task<Phase> FindConditionById(Expression<Func<Phase, bool>> predicate)
        {
            return await _context.Phases.SingleOrDefaultAsync(predicate);
        }

        public async Task<List<Phase>> GetAllPhases()
        {
            return await _context.Phases.ToListAsync();
        }

        public async Task<Phase> GetPhaseById(Guid id)
        {
            return await _context.Phases.SingleOrDefaultAsync(z => z.Id == id);
        }

        public void Insert(Phase phase)
        {
            _context.Phases.Add(phase);
        }

        public bool PhaseExists(Guid id)
        {
            return _context.Phases.Any(e => e.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Phase phase)
        {
            _context.Phases.Update(phase);
        }
    }
}
