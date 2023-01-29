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
    public class ProjectPhaseRepository : IProjectPhaseRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectPhaseRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void Delete(ProjectPhase projectPhase)
        {
            _context.ProjectPhases.Remove(projectPhase);
        }

        public async Task<List<ProjectPhase>> FindAllByCondition(Expression<Func<ProjectPhase, bool>> predicate)
        {
            return await _context.ProjectPhases
            .Include(p => p.Phase)
            .Include(b => b.Project)
            .Where(predicate)
            .ToListAsync();
        }

        public async Task<ProjectPhase> FindConditionById(Expression<Func<ProjectPhase, bool>> predicate)
        {
            return await _context.ProjectPhases
                .Include(p => p.Phase)
                .Include(b => b.Project)
                .SingleOrDefaultAsync(predicate);
        }

        public async Task<List<ProjectPhase>> GetAllProjectPhases()
        {
            return await _context.ProjectPhases
            .Include(p => p.Phase)
            .Include(b => b.Project)
            .ToListAsync();
        }

        public async Task<List<ProjectPhase>> GetAllProjectPhasesByProjectManagerId(string id)
        {
            return await _context.ProjectPhases
            .Include(p => p.Phase)
            .Include(b => b.Project)
            .Include(q=>q.Project.ProjectManager)
            .Where(r=>r.Project.ProjectManagerId==id)
            .ToListAsync();
        }

        public async Task<List<ProjectPhase>> GetAllSpecificProjectPhaseById(Guid? id)
        {
            return await _context.ProjectPhases
              .Include(p => p.Phase)
              .Include(b => b.Project)
              .Where(e => e.ProjectId == id)
              .ToListAsync();
        }

        public async Task<ProjectPhase> GetProjectPhaseById(Guid id)
        {
            return await _context.ProjectPhases
                .Include(p => p.Phase)
                .Include(b => b.Project)
                .SingleOrDefaultAsync(e => e.Id == id);
        }

        public void Insert(ProjectPhase projectPhase)
        {
            _context.ProjectPhases.Add(projectPhase);
        }

        public bool IsPhaseExist(Guid projectId,Guid phaseId)
        {
            var res = _context.ProjectPhases.SingleOrDefault(q => q.ProjectId == projectId);
            var item = _context.ProjectPhases
                .Where(e=>e.ProjectId==res.ProjectId)
                .Any(e=>e.PhaseId==phaseId);
            return item;
        }

        public bool IsPhaseExistForUpdate(Guid projectId, Guid phaseId)
        {
            //var res = _context.ProjectPhases.SingleOrDefault(q => q.ProjectId == projectId);
            var item = _context.ProjectPhases
                .Where(e => e.ProjectId == projectId)
                .Any(e => e.PhaseId == phaseId);
            return item;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ProjectPhase projectPhase)
        {
            _context.ProjectPhases.Update(projectPhase);
        }
    }
}
