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
    [Authorize(Roles = "Admin")]
    public class ProjectDirectorController : Controller
    {
        private readonly IProjectManagerRepository _projectManagerRepository;
        private readonly IProjectDirectorRepository _projectDirectorRepository;
        private readonly UserManager<Person> _userManager;
        private readonly IWebHostEnvironment _hosting;

        public ProjectDirectorController(IProjectManagerRepository projectManagerRepository, UserManager<Person> userManager, IWebHostEnvironment hosting, IProjectDirectorRepository projectDirectorRepository)
        {
            this._projectManagerRepository = projectManagerRepository;
            this._userManager = userManager;
            _hosting = hosting;
            _projectDirectorRepository = projectDirectorRepository;
        }

        // GET: ProjectDirectorController
        public async Task<ActionResult> Index()
        {
            var item = await _projectDirectorRepository.GetAllProjectDirectors();
            return View(item);
        }

        // GET: ProjectDirectorController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProjectDirectorController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectDirectorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(InsertProjectDirectorDTO insertProjectDirectorDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var projectDirector = new ProjectDirector()
                    {
                        Email = insertProjectDirectorDTO.Email,
                        UserName = insertProjectDirectorDTO.Email,
                        PhoneNumber = insertProjectDirectorDTO.PhoneNumber,
                        EmailConfirmed = true,
                        FullName = insertProjectDirectorDTO.FullName,
                    };
                    // to check if the user email exist or not
                    if (_projectDirectorRepository.CheckExist(projectDirector))
                    {
                        ViewBag.msg = false;
                        ModelState.AddModelError("", "Sorry the Email is already used");
                        return View();
                    }
                    // to add projectmanager account
                    UploadImage(projectDirector);
                    var res = await _userManager.CreateAsync(projectDirector, insertProjectDirectorDTO.Password);
                    // to add projectmanager role to the projectmanager account
                    var role = await _userManager.AddToRoleAsync(projectDirector, WebRoles.ProjectDirector);
                    TempData["save"] = "ProjectDirector has been Created Successfully ...";
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

        // GET: ProjectDirectorController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectDirectorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: ProjectDirectorController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectDirectorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
