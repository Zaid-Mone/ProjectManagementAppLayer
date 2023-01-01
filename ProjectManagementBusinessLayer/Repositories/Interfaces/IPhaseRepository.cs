using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IPhaseRepository
    {
        public Task<List<Phase>> GetAllPhases();
        public Task<Phase> GetPhaseById(Guid id);
        public void Insert(Phase phase);
        public void Update(Phase phase);
        public void Delete(Phase phase);
        public bool PhaseExists(Guid id);
        public void Save();
    }
}
