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
    public class ProjectManagerRepository : IProjectManagerRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectManagerRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public bool CheckExist(ProjectManager projectManager)
        {
            var check = _context.ProjectManagers.Any(r => r.Email == projectManager.Email);
            return check;
        }

        public async Task<List<ProjectManager>> FindAllByCondition(Expression<Func<ProjectManager, bool>> predicate)
        {
            return await _context.ProjectManagers.Where(predicate).ToListAsync();
        }

        public async Task<ProjectManager> FindConditionById(Expression<Func<ProjectManager, bool>> predicate)
        {
            return await _context.ProjectManagers.Include(p => p.Projects).SingleOrDefaultAsync(predicate);
        }

        public async Task<List<ProjectManager>> GetAllProjectManagers()
        {
            return await _context.ProjectManagers.ToListAsync();
        }

        public async Task<ProjectManager> GetProjectManagerById(string id)
        {
            var project = await _context.Projects
                .Include(q => q.ProjectManager)
                .Where(e => e.ProjectManagerId == id)
                .ToListAsync();
            return await _context.ProjectManagers.Include(p=>p.Projects).SingleOrDefaultAsync(t=>t.Id==id);
        }

    }
}
