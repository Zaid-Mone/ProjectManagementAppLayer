using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAppLayer.DTOs.Insert;
using ProjectManagementAppLayer.Utility;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly IAdminRepository _adminRepository;
        private readonly UserManager<Person> _userManager;

        public AdminController(IAdminRepository adminRepository, 
            UserManager<Person> userManager)
        {
            _adminRepository = adminRepository;
            _userManager = userManager;
        }

        // GET: AdminController
        public async Task<ActionResult> Index()
        {
            var admin = await _adminRepository.GetAllAdmins();
            return View(admin);
        }

        // GET: Administrator/AdminController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            var admin = await _adminRepository.GetAdminById(id);
            return View(admin);
        }

        // GET: Administrator/AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Administrator/AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InsertAdminDTO adminDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var admin = new Admin()
                    {
                        Email = adminDTO.Email,
                        UserName = adminDTO.Email,
                        PhoneNumber = adminDTO.PhoneNumber,
                        EmailConfirmed = true,
                        FullName = adminDTO.FullName,
                    };
                    var check = _adminRepository.CheckExist(admin);
                    var user = await _userManager.FindByEmailAsync(admin.Email);
                    if (user!=null || check)
                    {
                        ViewBag.msg = false;
                        ModelState.AddModelError("", "Sorry the Email is already used");
                        return View();
                    }
                    var res = await _userManager.CreateAsync(admin, adminDTO.Password);
                    // to add projectmanager role to the projectmanager account
                    var role = await _userManager.AddToRoleAsync(admin, WebRoles.Admin);
                    TempData["save"] = "Admin has been Created Successfully ...";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("", "Something Went Wrong");
                    return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Administrator/AdminController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var admin = await _adminRepository.GetAdminById(id);
            return View(admin);

        }

        // POST: Administrator/AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Admin admin)
        {
            try
            {
                var administrator = await _adminRepository.GetAdminById(admin.Id);
                if (administrator == null)
                {
                    return NotFound();
                }
                var res = await _userManager.FindByEmailAsync(administrator.Email);
                if (res == null)
                {
                    return NotFound();
                }
                await _userManager.DeleteAsync(res);
                TempData["delete"] = "Admin has been Deleted Successfully ...";
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
