using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IProjectPhaseRepository
    {
        public Task<List<ProjectPhase>> GetAllProjectPhases();
        public Task<List<ProjectPhase>> GetAllProjectPhasesByProjectManagerId(string id);
        public Task<ProjectPhase> GetProjectPhaseById(Guid id);
        public Task<List<ProjectPhase>> GetAllSpecificProjectPhaseById(Guid? id);
        public bool IsPhaseExist(Guid projectId, Guid phaseId);
        public bool IsPhaseExistForUpdate(Guid projectId, Guid phaseId);
        public void Insert(ProjectPhase projectPhase);
        public void Update(ProjectPhase projectPhase);
        public void Delete(ProjectPhase projectPhase);
        public void Save();
        public Task<List<ProjectPhase>> FindAllByCondition(Expression<Func<ProjectPhase, bool>> predicate);
        public Task<ProjectPhase> FindConditionById(Expression<Func<ProjectPhase, bool>> predicate);
    }

}
