using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IClientRepository
    {
        public Task<List<Client>> GetAllClients();
        public Task<Client> GetClientById(Guid id);
        public Task<List<Client>> FindAllByCondition(Expression<Func<Client, bool>> predicate);
        public Task<Client> FindConditionById(Expression<Func<Client, bool>> predicate);
        public void Insert(Client client);
        public void Update(Client client);
        public void Delete(Client client);
        public bool ClientExists(Guid id);
        public void Save();
    }
}
