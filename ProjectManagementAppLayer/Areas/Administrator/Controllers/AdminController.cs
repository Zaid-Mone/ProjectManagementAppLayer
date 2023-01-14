using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAppLayer.DTOs.Insert;
using ProjectManagementAppLayer.Utility;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IAdminRepository _adminRepository;
        private readonly UserManager<Person> _userManager;
        private readonly IWebHostEnvironment _hosting;
        private readonly IProjectRepository _projectRepository;
        public AdminController(IAdminRepository adminRepository,
            UserManager<Person> userManager, IWebHostEnvironment hosting, ApplicationDbContext db, IProjectRepository projectRepository)
        {
            _adminRepository = adminRepository;
            _userManager = userManager;
            _hosting = hosting;
            _db = db;
            _projectRepository = projectRepository;
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
                    UploadImage(admin);
                    var res = await _userManager.CreateAsync(admin, adminDTO.Password);
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

        private void UploadImage(Person model)
        {
            var file = HttpContext.Request.Form.Files;
            if (file.Count() > 0)
            {//@"wwwroot/"
                string ImageName = Guid.NewGuid().ToString() + Path.GetExtension(file[0].FileName);
                var filestream = new FileStream(Path.Combine(_hosting.WebRootPath, "Images", ImageName), FileMode.Create);
                file[0].CopyTo(filestream);
                model.ImageUrl = ImageName;
            }
            else if (model.ImageUrl == null)
            {
                model.ImageUrl = "noImage.png";
            }
            else
            {
                model.ImageUrl = model.ImageUrl;
            }
        }

        
        public IActionResult Statistics()
        {
            //var usr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.projectCount = _db.Projects
                .Include(a => a.ProjectStatus)
                .Include(t => t.ProjectType)
                .ToList().Count;


            // for count all user payements
            var res = _db.PaymentTerms
                .Include(q => q.Deliverable)
                .ThenInclude(r => r.ProjectPhase)
                .ThenInclude(v => v.Project)
                .ToList();
            decimal sum = 0;
            foreach (var item in res)
            {
                sum += item.PaymentTermAmount;
            }
            ViewBag.sumation = sum.ToString("C");

            var pstatus = _db.ProjectStatuses.ToList();

            // get count of pending projects
            var res1 = _db.Projects
               .Include(e => e.ProjectStatus)
               .Where(y => y.ProjectStatus.Status == "Pending").ToList().Count;

            ViewBag.status = res1;

            // get count of active projects
            var res2 = _db.Projects
               .Include(e => e.ProjectStatus)
               .Where(y => y.ProjectStatus.Status == "Active").ToList().Count;

            ViewBag.active = res2;

            return View();
        }


     //   Administrator/Admin/GetAllProjectsForAdmin
        public async Task<JsonResult> GetAllProjectsForAdmin()
        {
            var project = await _projectRepository.GetAllProjects();
            return new JsonResult(project);
        }
    }
}
