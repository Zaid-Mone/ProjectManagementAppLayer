using ProjectManagementBusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        public Task<List<Admin>> GetAllAdmins();
        //public Task<List<Admin>> GetAllOnlineUsers();
        public Task<Admin> GetAdminById(string id);
        public bool CheckExist(Admin admin);
        public Task<List<Admin>> FindAllByCondition(Expression<Func<Admin, bool>> predicate);
        public Task<Admin> FindConditionById(Expression<Func<Admin, bool>> predicate);

    }
}
