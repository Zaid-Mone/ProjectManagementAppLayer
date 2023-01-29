using Microsoft.EntityFrameworkCore;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Implementation
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public bool ClientExists(Guid id)
        {
            return _context.Clients.Any(e => e.Id == id);
        }

        public void Delete(Client client)
        {
            _context.Clients.Remove(client);
        }

        public async Task<List<Client>> FindAllByCondition(Expression<Func<Client, bool>> predicate)
        {
            return await _context.Clients.Where(predicate).ToListAsync();
        }

        public async Task<Client> FindConditionById(Expression<Func<Client, bool>> predicate)
        {
            return await _context.Clients.SingleOrDefaultAsync(predicate);
        }

        public async Task<List<Client>> GetAllClients()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetClientById(Guid id)
        {
            return await _context.Clients.SingleOrDefaultAsync(r=>r.Id==id);
        }

        public void Insert(Client client)
        {
            _context.Clients.Add(client);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Update(Client client)
        {
            _context.Clients.Update(client);
        }
    }
}
