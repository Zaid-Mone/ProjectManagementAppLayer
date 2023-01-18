using ProjectManagementBusinessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IProjectDirectorRepository
    {
        public Task<List<ProjectDirector>> GetAllProjectDirectors();
        public Task<ProjectDirector> GetProjectDirectorById(string id);
        public bool CheckExist(ProjectDirector projectDirector);
        //public void Delete(ProjectManager projectManager);
        
    }    
}
