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
    public class ProjectTypeRepository : IProjectTypeRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectTypeRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Delete(ProjectType projectType)
        {
            _context.ProjectTypes.Remove(projectType);
        }

        public async Task<List<ProjectType>> GetAllProjectTypes()
        {
            return await _context.ProjectTypes.ToListAsync();
        }

        public async Task<ProjectType> GetProjectTypeById(Guid id)
        {
            return await _context.ProjectTypes.SingleOrDefaultAsync(r=>r.Id==id);
        }

        public void Insert(ProjectType projectType)
        {
            _context.ProjectTypes.Add(projectType);
        }

        public bool ProjectTypeExists(Guid id)
        {
            return _context.ProjectTypes.Any(e => e.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ProjectType projectType)
        {
            _context.ProjectTypes.Update(projectType);
        }
    }
}
