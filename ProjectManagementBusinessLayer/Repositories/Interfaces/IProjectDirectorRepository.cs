using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IProjectDirectorRepository
    {
        public Task<List<ProjectDirector>> GetAllProjectDirectors();
        public Task<ProjectDirector> GetProjectDirectorById(string id);
        public bool CheckExist(ProjectDirector projectDirector);
        //public void Delete(ProjectManager projectManager);
        public Task<List<ProjectDirector>> FindAllByCondition(Expression<Func<ProjectDirector, bool>> predicate);
        public Task<ProjectDirector> FindConditionById(Expression<Func<ProjectDirector, bool>> predicate);
    }    
}
