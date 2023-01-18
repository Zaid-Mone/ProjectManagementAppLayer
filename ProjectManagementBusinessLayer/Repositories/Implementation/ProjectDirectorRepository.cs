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
    public class ProjectDirectorRepository : IProjectDirectorRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectDirectorRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public bool CheckExist(ProjectDirector projectDirector)
        {
            var check = _context.ProjectDirectors.Any(r => r.Email == projectDirector.Email);
            return check;
        }

        public async Task<List<ProjectDirector>> GetAllProjectDirectors()
        {
            return await _context.ProjectDirectors.ToListAsync();
        }

        public async Task<ProjectDirector> GetProjectDirectorById(string id)
        {
            var project = await _context.Projects
                .Include(q => q.ProjectManager)
                .Where(e => e.ProjectManagerId == id)
                .ToListAsync();
            return await _context.ProjectDirectors
                .Include(p => p.Projects)
                .SingleOrDefaultAsync(t => t.Id == id);
        }
    }
}
