using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAppLayer.DTOs.Insert;
using ProjectManagementAppLayer.DTOs.Update;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;

namespace ProjectManagementAppLayer.Areas.ProjectManagment.Controllers
{
    [Area("ProjectManagment")]
    public class ProjectController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IClientRepository _clientRepository;
        private readonly UserManager<Person> _userManager;
        private readonly IProjectPhaseRepository _projectPhaseRepository;

        public ProjectController(/*ApplicationDbContext context,*/
            IProjectRepository projectRepository, IProjectTypeRepository projectTypeRepository, IProjectStatusRepository projectStatusRepository, IClientRepository clientRepository, UserManager<Person> userManager, IProjectPhaseRepository projectPhaseRepository)
        {
            //_context = context;
            this._projectRepository = projectRepository;
            _projectTypeRepository = projectTypeRepository;
            _projectStatusRepository = projectStatusRepository;
            this._clientRepository = clientRepository;
            _userManager = userManager;
            _projectPhaseRepository = projectPhaseRepository;
        }

        // GET: ProjectManagment/Project
        public async Task<IActionResult> Index()
        {
          var res =  await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Admin");
            if (res)
            {
                var item = await _projectRepository.GetAllProjects();
                return View(item);
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var applicationDbContext = await _projectRepository.GetProjectManagerProjects(userId);
                return View(applicationDbContext);
            }
        }

        // GET: ProjectManagment/Project/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _projectRepository.GetProjectById(id);
            var projectphases = await _projectPhaseRepository.GetAllSpecificProjectPhaseById(project.Id);
            project.ProjectPhases = projectphases;
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: ProjectManagment/Project/Create
        public async Task<IActionResult> Create()
        {

            ViewBag.projectStatus = await _projectStatusRepository.GetAllProjectStatuses();
            ViewBag.projectType = await _projectTypeRepository.GetAllProjectTypes();
            ViewBag.client = await _clientRepository.GetAllClients();
            //dbcontext.Database.ExecuteSqlCommand("Select c.Name,c.Email from Projects s right join Clients c on s.ClientId = c.Id where s.ClientId is null; ", user);

            return View();


        }

        // POST: ProjectManagment/Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsertProjectDTO insertProjectDTO)
        {
            if (ModelState.IsValid)
            {
                var project = new Project()
                {
                    Id = new Guid(),
                    EndDate = insertProjectDTO.EndDate,
                    StartDate = insertProjectDTO.StartDate,
                    ContractAmount = insertProjectDTO.ContractAmount,
                    ProjectName = insertProjectDTO.ProjectName,
                    ProjectStatusId = insertProjectDTO.ProjectStatusId,
                    ProjectTypeId = insertProjectDTO.ProjectTypeId,
                    ContractFileName = insertProjectDTO.ContractFile.FileName,
                    ContractFileType = insertProjectDTO.ContractFile.ContentType,
                    ClientId=insertProjectDTO.ClientId,
                    ProjectManagerId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };
                Stream st = insertProjectDTO.ContractFile.OpenReadStream();
                using (BinaryReader bt = new BinaryReader(st))
                {
                    var byteFile = bt.ReadBytes((int)st.Length);
                    project.ContractFile = byteFile;
                     _projectRepository.Insert(project);
                    _projectRepository.Save();
                    TempData["save"] = "Project has been Created Successfully ...";
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ViewBag.projectStatus = await _projectStatusRepository.GetAllProjectStatuses();
                ViewBag.projectType = await _projectTypeRepository.GetAllProjectTypes();
                ViewBag.client = await _clientRepository.GetAllClients();
                ModelState.AddModelError("", "Something went wrong");
                return View(insertProjectDTO);
            }

        }

        // GET: ProjectManagment/Project/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _projectRepository.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }
            ViewBag.project = project;
            var projectStatus = await _projectStatusRepository.GetAllProjectStatuses();
            ViewBag.projectStatus = projectStatus;
           var projectTypes = await _projectTypeRepository.GetAllProjectTypes();
            ViewBag.projectType = projectTypes;
          var clients = await _clientRepository.GetAllClients();
            ViewBag.client = clients;

            ViewData["ClientId"] = new SelectList(clients, "Id", "Name", project.ClientId);
            ViewData["ProjectStatusId"] = new SelectList(projectStatus, "Id", "Status", project.ProjectStatusId);
            ViewData["ProjectTypeId"] = new SelectList(projectTypes, "Id", "Type", project.ProjectTypeId);
            return View();
        }

        // POST: ProjectManagment/Project/Edit/5
        [HttpPost] // solve the error when update the file deleted
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProjectDTO updateProjectDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var proj = await _projectRepository.GetProjectById(updateProjectDTO.ProjectId);
                    if (updateProjectDTO.ContractFile == null)
                    {
                        proj.ClientId = updateProjectDTO.ClientId;
                        proj.ContractAmount = updateProjectDTO.ContractAmount;
                        proj.StartDate = updateProjectDTO.StartDate;
                        proj.EndDate = updateProjectDTO.EndDate;
                        proj.ProjectManagerId = updateProjectDTO.ProjectManagerId;
                        proj.ProjectStatusId = updateProjectDTO.ProjectStatusId;
                        proj.ProjectTypeId = updateProjectDTO.ProjectTypeId;
                        proj.ProjectName = updateProjectDTO.ProjectName;
                        _projectRepository.Update(proj);
                        _projectRepository.Save();
                        TempData["edit"] = "Project has been Updated Successfully ...";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        proj.ClientId = updateProjectDTO.ClientId;
                        proj.ContractAmount = updateProjectDTO.ContractAmount;
                        proj.StartDate = updateProjectDTO.StartDate;
                        proj.EndDate = updateProjectDTO.EndDate;
                        proj.ProjectManagerId = updateProjectDTO.ProjectManagerId;
                        proj.ProjectStatusId = updateProjectDTO.ProjectStatusId;
                        proj.ProjectTypeId = updateProjectDTO.ProjectTypeId;
                        proj.ProjectName = updateProjectDTO.ProjectName;
                        proj.ContractFileName = updateProjectDTO.ContractFile.FileName;
                        proj.ContractFileType = updateProjectDTO.ContractFile.ContentType;

                        Stream st = updateProjectDTO.ContractFile.OpenReadStream();
                        using (BinaryReader bt = new BinaryReader(st))
                        {
                            var byteFile = bt.ReadBytes((int)st.Length);
                            proj.ContractFile = byteFile;
                            _projectRepository.Update(proj);
                            _projectRepository.Save();
                            TempData["edit"] = "Project has been Updated Successfully ...";
                            return RedirectToAction(nameof(Index));
                        }

                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (updateProjectDTO.ProjectId==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                
            }
            ViewBag.projectStatus = await _projectStatusRepository.GetAllProjectStatuses();
            ViewBag.projectType = await _projectTypeRepository.GetAllProjectTypes();
            ViewBag.client = await _clientRepository.GetAllClients();
            return View();
        }

        // GET: ProjectManagment/Project/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _projectRepository.GetProjectById(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: ProjectManagment/Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var project = await _projectRepository.GetProjectById(id);
           _projectRepository.Delete(project);
            _projectRepository.Save();
            TempData["delete"] = "Project has been Deleted Successfully ...";
            return RedirectToAction(nameof(Index));
        }




        public async Task<FileStreamResult> ViewContract(Guid id) // to open connection with browser
        {
            var file = await _projectRepository.GetProjectById(id);
            Stream stream = new MemoryStream(file.ContractFile); // array of bytes 
            return new FileStreamResult(stream, file.ContractFileType);
        }


        //[HttpPost] // solve the error when update the file deleted
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(UpdateProjectDTO updateProjectDTO)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            //var proj = await _projectRepository.GetProjectById(updateProjectDTO.ProjectId);
        //            if (updateProjectDTO.ContractFile != null)
        //            {
        //                var project = new Project()
        //                {
        //                    Id = updateProjectDTO.ProjectId,
        //                    ClientId = updateProjectDTO.ClientId,
        //                    ContractAmount = updateProjectDTO.ContractAmount,
        //                    StartDate = updateProjectDTO.StartDate,
        //                    EndDate = updateProjectDTO.EndDate,
        //                    ProjectManagerId = updateProjectDTO.ProjectManagerId,
        //                    ProjectName = updateProjectDTO.ProjectName,
        //                    ProjectStatusId = updateProjectDTO.ProjectStatusId,
        //                    ProjectTypeId = updateProjectDTO.ProjectTypeId,
        //                    ContractFileName = updateProjectDTO.ContractFile.FileName,
        //                    ContractFileType = updateProjectDTO.ContractFile.ContentType,

        //                };
        //                Stream st = updateProjectDTO.ContractFile.OpenReadStream();
        //                using (BinaryReader bt = new BinaryReader(st))
        //                {
        //                    var byteFile = bt.ReadBytes((int)st.Length);
        //                    project.ContractFile = byteFile;
        //                    _projectRepository.Update(project);
        //                    _projectRepository.Save();
        //                    TempData["edit"] = "Project has been Updated Successfully ...";
        //                    return RedirectToAction(nameof(Index));
        //                }
        //            }
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (updateProjectDTO.ProjectId == null)
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }

        //    }
        //    ViewBag.projectStatus = await _projectStatusRepository.GetAllProjectStatuses();
        //    ViewBag.projectType = await _projectTypeRepository.GetAllProjectTypes();
        //    ViewBag.client = await _clientRepository.GetAllClients();
        //    return View();
        //}

    }
}
