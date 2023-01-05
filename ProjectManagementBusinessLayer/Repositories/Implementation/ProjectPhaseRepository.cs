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
