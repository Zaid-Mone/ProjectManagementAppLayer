using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        // Retrieve All Admin Users
        public Task<List<Admin>> GetAllAdmins();
        //// Retrieve All Online Users
        //public Task<List<Admin>> GetAllOnlineUsers();
        // Bring Admin By His Own Id
        public Task<Admin> GetAdminById(string id);
        // Check if the Admin Email Is Already Exist
        public bool CheckExist(Admin admin);
        // Find All Admin Based On A Specific Condition
        public Task<List<Admin>> FindAllByCondition(Expression<Func<Admin, bool>> predicate);
        // Find Admin Based On A Specific Condition
        public Task<Admin> FindConditionById(Expression<Func<Admin, bool>> predicate);
    }
}
