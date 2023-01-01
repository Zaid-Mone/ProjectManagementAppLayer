using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IProjectStatusRepository
    {
        public Task<List<ProjectStatus>> GetAllProjectStatuses();
        public Task<ProjectStatus> GetProjectStatusById(Guid id);
        public void Insert(ProjectStatus projectStatus);
        public void Update(ProjectStatus projectStatus);
        public void Delete(ProjectStatus projectStatus);
        public bool ProjectStatusExists(Guid id);
        public void Save();
    }
}
