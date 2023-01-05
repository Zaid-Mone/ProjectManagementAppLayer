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
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            this._context = context;
        }
        public void Delete(Project project)
        {
            _context.Projects.Remove(project);
        }

        public async Task<List<Project>> GetAllProjects()
        {
         
            return await _context.Projects
                .Include(z=>z.Client)
                .Include(q=>q.ProjectManager)
                .Include(t => t.ProjectStatus)
                .Include(s => s.ProjectType)
                .Include(p => p.ProjectPhases)
                .ThenInclude(b => b.Phase)
                .ToListAsync();
        }

        public async Task<Project> GetPhaseByProjectId(Guid? id)
        {
            return await _context.Projects
            .Include(z => z.Client)
            .Include(q => q.ProjectManager)
            .Include(z => z.ProjectStatus)
            .Include(x => x.ProjectType)
            .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Project> GetProjectById(Guid id)
        {
            return await _context.Projects
            .Include(z => z.Client)
            .Include(q => q.ProjectManager)
            .Include(z => z.ProjectStatus)
            .Include(x => x.ProjectType)
            .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<Project> GetProjectBySpecificId(Guid? id)
        {
            return await _context.Projects
                .Include(z => z.Client)
                .Include(q => q.ProjectManager)
                .Include(z => z.ProjectStatus)
                .Include(x => x.ProjectType)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Project>> GetProjectManagerProjects(string userId)
        {
            return await _context.Projects
           .Include(z => z.Client)
           .Include(q => q.ProjectManager)
           .Include(t => t.ProjectStatus)
           .Include(s => s.ProjectType)
           .Include(p => p.ProjectPhases)
           .ThenInclude(b => b.Phase)
           .Where(e => e.ProjectManagerId == userId)
           .ToListAsync();
        }

        public void Insert(Project project)
        {
            _context.Projects.Add(project);
        }

        public bool ProjectExists(Guid id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Project project)
        {
            _context.Projects.Update(project);
        }
    }
}
