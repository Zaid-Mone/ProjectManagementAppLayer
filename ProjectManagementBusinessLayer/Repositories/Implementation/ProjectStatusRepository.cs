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
    public class ProjectStatusRepository : IProjectStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectStatusRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public void Delete(ProjectStatus projectStatus)
        {
            _context.ProjectStatuses.Remove(projectStatus);
        }

        public async Task<List<ProjectStatus>> GetAllProjectStatuses()
        {
            return await _context.ProjectStatuses.ToListAsync();
        }

        public async Task<ProjectStatus> GetProjectStatusById(Guid id)
        {
            return await _context.ProjectStatuses.SingleOrDefaultAsync(t=>t.Id==id);
        }

        public void Insert(ProjectStatus projectStatus)
        {
            _context.ProjectStatuses.Add(projectStatus);
        }

        public bool ProjectStatusExists(Guid id)
        {
            return _context.ProjectStatuses.Any(e => e.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(ProjectStatus projectStatus)
        {
            _context.ProjectStatuses.Update(projectStatus);
        }
    }
}
