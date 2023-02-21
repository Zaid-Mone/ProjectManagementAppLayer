using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectTypeRepository _projectTypeRepository;
        private readonly IProjectStatusRepository _projectStatusRepository;
        private readonly IClientRepository _clientRepository;
        private readonly UserManager<Person> _userManager;
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IMapper _mapper;
        public ProjectController(
            ApplicationDbContext context,
            IProjectRepository projectRepository,
            IProjectTypeRepository projectTypeRepository,
            IProjectStatusRepository projectStatusRepository,
            IClientRepository clientRepository,
            UserManager<Person> userManager,
            IProjectPhaseRepository projectPhaseRepository, IMapper mapper)
        {
            _context = context;
            this._projectRepository = projectRepository;
            _projectTypeRepository = projectTypeRepository;
            _projectStatusRepository = projectStatusRepository;
            this._clientRepository = clientRepository;
            _userManager = userManager;
            _projectPhaseRepository = projectPhaseRepository;
            _mapper = mapper;
        }

        // GET: ProjectManagment/Project
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var res = await (_userManager.IsInRoleAsync(user, "Admin")) ||
                await (_userManager.IsInRoleAsync(user, "ProjectDirector"));
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
            return View();


        }

        // POST: ProjectManagment/Project/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsertProjectDTO insertProjectDTO)
        {
            if (ModelState.IsValid)
            {
                // aut mapper
                var project = _mapper.Map<Project>(insertProjectDTO);
                project.ContractFileName = insertProjectDTO.ContractFile.FileName;
                project.ContractFileType = insertProjectDTO.ContractFile.ContentType;
                project.ProjectManagerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
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


        // update the flag
        public async Task<IActionResult> ProjectApproved(Guid id)
        {
            var project = await _projectRepository.GetProjectById(id);
            project.IsApproved = true;
            _projectRepository.Update(project);
            _projectRepository.Save();
            ViewBag.msg = true;
            return RedirectToAction("GetAllPendingProjects", "Project");
        }
        // update the flag
        public async Task<IActionResult> ProjectPending(Guid id)
        {
            var project = await _projectRepository.GetProjectById(id);
            project.IsApproved = false;
            _projectRepository.Update(project);
            _projectRepository.Save();
            return RedirectToAction("GetAllApprovedProjects", "Project"); 
        }

        // to return all pending project only
        public async Task<IActionResult> GetAllPendingProjects()
        {
            var item = await _projectRepository.GetAllPendingProjects();
            return View(item);
        }
        // to return all approved project only
        public async Task<IActionResult> GetAllApprovedProjects()
        {
            var item = await _projectRepository.GetAllApprovedProjects();
            return View(item);
        }

        // get all projects for  admin & Director 
        public async Task<JsonResult> GetAllProjectForCalenderAdminAndProjectDirector()
        {
            if (User.IsInRole("Admin") || User.IsInRole("ProjectDirector"))
            {
                var project = await _context.Projects
                .Select(proj =>
                new
                {
                    ProjectName = proj.ProjectName,
                    StartDate = proj.StartDate,
                    EndDate=proj.EndDate
                }).ToListAsync();


                return new JsonResult(project);
            }
            else
            {
                return await GetAllProjectForCalenderProjectManager();
            }
        }
        // get all projects for specific manager
        public async Task<JsonResult> GetAllProjectForCalenderProjectManager()
        {
            if (User.IsInRole("ProjectManager"))
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var applicationDbContext = await _projectRepository.GetProjectManagerProjects(userId);
                return new JsonResult(applicationDbContext);
            }
            else
            {
                return await GetAllProjectForCalenderAdminAndProjectDirector();
            }

        }

    }
}
