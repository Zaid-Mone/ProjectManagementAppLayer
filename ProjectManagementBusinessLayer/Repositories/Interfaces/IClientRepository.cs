using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IClientRepository
    {
        public Task<List<Client>> GetAllClients();
        public Task<Client> GetClientById(Guid id);
        public void Insert(Client client);
        public void Update(Client client);
        public void Delete(Client client);
        public void Save();
    }
}
