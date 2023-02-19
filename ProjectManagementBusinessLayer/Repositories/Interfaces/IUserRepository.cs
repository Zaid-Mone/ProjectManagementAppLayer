using ProjectManagementBusinessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<List<Person>> GetAllUsers();
    }
}
