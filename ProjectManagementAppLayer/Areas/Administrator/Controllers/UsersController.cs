using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        { 
            var users = await _userRepository.GetAllUsers();
            return View(users);
        }
    }
}
