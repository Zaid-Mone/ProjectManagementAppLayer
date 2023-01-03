using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IProjectPhaseRepository
    {
        public Task<List<ProjectPhase>> GetAllProjectPhases();
        public Task<ProjectPhase> GetProjectPhaseById(Guid id);
        public Task<List<ProjectPhase>> GetAllSpecificProjectPhaseById(Guid? id);
        public void Insert(ProjectPhase projectPhase);
        public void Update(ProjectPhase projectPhase);
        public void Delete(ProjectPhase projectPhase);
        public void Save();
    }

}
