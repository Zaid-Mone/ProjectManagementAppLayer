using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IPhaseRepository
    {
        public Task<List<Phase>> GetAllPhases();
        public Task<Phase> GetPhaseById(Guid id);
        public Task<List<Phase>> FindAllByCondition(Expression<Func<Phase, bool>> predicate);
        public Task<Phase> FindConditionById(Expression<Func<Phase, bool>> predicate);
        public void Insert(Phase phase);
        public void Update(Phase phase);
        public void Delete(Phase phase);
        public bool PhaseExists(Guid id);
        public void Save();
    }
}
