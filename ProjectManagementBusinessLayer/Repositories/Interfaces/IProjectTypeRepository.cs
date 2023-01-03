using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IProjectTypeRepository
    {
        public Task<List<ProjectType>> GetAllProjectTypes();
        public Task<ProjectType> GetProjectTypeById(Guid id);
        public void Insert(ProjectType projectType);
        public void Update(ProjectType projectType);
        public void Delete(ProjectType projectType);
        public bool ProjectTypeExists(Guid id);
        public void Save();
    }
}
