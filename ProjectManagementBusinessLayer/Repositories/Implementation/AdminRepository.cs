using Microsoft.EntityFrameworkCore;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementBusinessLayer.Repositories.Implementation
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _context;

        public AdminRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public bool CheckExist(Admin admin)
        {
            var check = _context.Admins.Any(r => r.Email == admin.Email);
            return check;
        }

        public async Task<Admin> GetAdminById(string id)
        {
            return await _context.Admins.SingleOrDefaultAsync(q => q.Id == id);
        }

        public async Task<List<Admin>> GetAllAdmins()
        {
            return await _context.Admins.ToListAsync();
        }
    }
}
