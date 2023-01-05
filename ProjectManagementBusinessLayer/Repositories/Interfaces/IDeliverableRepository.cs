using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IDeliverableRepository
    {
        public Task<List<Deliverable>> GetAllDeliverables();
        public Task<List<Deliverable>> GetAllDeliverableForProjectManager(string id);
        public Task<Deliverable> GetDeliverableById(Guid id);
        public void Insert(Deliverable deliverable);
        public void Update(Deliverable deliverable);
        public void Delete(Deliverable deliverable);
        public void Save();
    }
}
