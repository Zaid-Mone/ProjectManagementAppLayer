using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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
        public Task<List<ProjectStatus>> FindAllByCondition(Expression<Func<ProjectStatus, bool>> predicate);
        public Task<ProjectStatus> FindConditionById(Expression<Func<ProjectStatus, bool>> predicate);
    }
}
