using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IProjectManagerRepository
    {
        public Task<List<ProjectManager>> GetAllProjectManagers();
        public Task<ProjectManager> GetProjectManagerById(string id);
        public bool CheckExist(ProjectManager projectManager);
    }
}
