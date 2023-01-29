using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IProjectManagerRepository
    {
        public Task<List<ProjectManager>> GetAllProjectManagers();
        public Task<ProjectManager> GetProjectManagerById(string id);
        public bool CheckExist(ProjectManager projectManager);
        //public void Delete(ProjectManager projectManager);
        public Task<List<ProjectManager>> FindAllByCondition(Expression<Func<ProjectManager, bool>> predicate);
        public Task<ProjectManager> FindConditionById(Expression<Func<ProjectManager, bool>> predicate);
    }    
}
