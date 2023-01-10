using ProjectManagementBusinessLayer.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        public Task<List<Admin>> GetAllAdmins();
        public Task<Admin> GetAdminById(string id);
        public bool CheckExist(Admin admin);

        
    }
}
