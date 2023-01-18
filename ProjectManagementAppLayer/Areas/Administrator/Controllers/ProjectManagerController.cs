using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementAppLayer.DTOs.Insert;
using ProjectManagementAppLayer.Utility;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles ="Admin")]
    public class ProjectManagerController : Controller
    {
        private readonly IProjectManagerRepository _projectManagerRepository;
        private readonly UserManager<Person> _userManager;
        private readonly IWebHostEnvironment _hosting;

        public ProjectManagerController(IProjectManagerRepository projectManagerRepository, UserManager<Person> userManager, IWebHostEnvironment hosting)
        {
            this._projectManagerRepository = projectManagerRepository;
            this._userManager = userManager;
            _hosting = hosting;
        }
        // GET: ProjectManagerController
        public async Task<ActionResult> Index()
        {

            var item = await _projectManagerRepository.GetAllProjectManagers();
            return View(item);
        }

        // GET: ProjectManagerController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProjectManagerController/Create
        public ActionResult Create()
        {
            
            return View();
        }

        // POST: ProjectManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InsertProjectManagerDTO insertProjectManagerDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var projectmanager = new ProjectManager()
                    {
                        Email = insertProjectManagerDTO.Email,
                        UserName = insertProjectManagerDTO.Email,
                        PhoneNumber= insertProjectManagerDTO.PhoneNumber,
                        EmailConfirmed=true,
                        FullName= insertProjectManagerDTO.FullName,
                    };
                    // to check if the user email exist or not
                    if (_projectManagerRepository.CheckExist(projectmanager)) {
                        ViewBag.msg = false;
                        ModelState.AddModelError("", "Sorry the Email is already used");
                        return View();
                    }
                    // to add projectmanager account
                    UploadImage(projectmanager);
                    var res = await _userManager.CreateAsync(projectmanager, insertProjectManagerDTO.Password);
                    // to add projectmanager role to the projectmanager account
                    var role = await _userManager.AddToRoleAsync(projectmanager, WebRoles.ProjectManager);
                    TempData["save"] = "ProjectManager has been Created Successfully ...";
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

        // GET: ProjectManagerController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectManagerController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            var projectManager = await _projectManagerRepository.GetProjectManagerById(id);
            return View(projectManager);
        }

        // POST: ProjectManagerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(ProjectManager projectManager)
        {
            try
            {
                var projecM = await _projectManagerRepository.GetProjectManagerById(projectManager.Id);
                if (projecM == null)
                {
                    return NotFound();
                }
               var res =  await _userManager.FindByEmailAsync(projecM.Email);
                if(res == null)
                {
                    return NotFound();
                }
                await _userManager.DeleteAsync(res);
                TempData["delete"] = "ProjectManager has been Deleted Successfully ...";
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
                model.ImageUrl = "userPics.png";
            }
            else
            {
                model.ImageUrl = model.ImageUrl;
            }
        }

    }
}
