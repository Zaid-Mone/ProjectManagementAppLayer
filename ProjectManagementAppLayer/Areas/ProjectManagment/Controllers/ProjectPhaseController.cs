using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    public class ProjectPhaseController : Controller
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IPhaseRepository _phaseRepository;
        public ProjectPhaseController( IProjectRepository projectRepository, IProjectPhaseRepository projectPhaseRepository, IPhaseRepository phaseRepository)
        {
            _projectRepository = projectRepository;
            _projectPhaseRepository = projectPhaseRepository;
            _phaseRepository = phaseRepository;
        }

        // GET: ProjectManagment/ProjectPhase
        public async Task<IActionResult> Index(Guid? id)
        {
            if(id != null)
            {
                ViewBag.project = await _projectRepository.GetProjectBySpecificId(id);
                var applicationDbContext = await _projectPhaseRepository.GetAllSpecificProjectPhaseById(id);
                return View(applicationDbContext);
            }
            else
            {
                return View(await _projectPhaseRepository.GetAllProjectPhases());
            }

        }

        // GET: ProjectManagment/ProjectPhase/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectPhase = await _projectPhaseRepository.GetProjectPhaseById(id);
            if (projectPhase == null)
            {
                return NotFound();
            }

            return View(projectPhase);
        }

        // GET: ProjectManagment/ProjectPhase/Create
        public async Task<IActionResult> Create(Guid id)
        {
            var project = await _projectRepository.GetProjectById(id);
            ViewBag.project = project;
            var phases = await _phaseRepository.GetAllPhases();
            ViewBag.phase = phases;
            return View();
        }

        // POST: ProjectManagment/ProjectPhase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsertProjectPhaseDTO insertProjectPhaseDTO)
        {
            if (ModelState.IsValid)
            {
                var projcet = await _projectRepository.GetProjectById(insertProjectPhaseDTO.ProjectId);
                var phases = await _phaseRepository.GetAllPhases();
                var projectPhase = await _projectPhaseRepository.GetAllSpecificProjectPhaseById(insertProjectPhaseDTO.ProjectId);

                if (projcet.StartDate > insertProjectPhaseDTO.StartDate &&
                    projcet.EndDate > insertProjectPhaseDTO.EndDate)
                {
                    ViewBag.msg = false;
                    //ModelState.AddModelError("", $"The Start Date must be bigger Than or equal {projcet.StartDate.ToString("d")} && The End Date must be less Than or equal {projcet.EndDate.ToString("d")}");
                    ViewBag.project = projcet;
                    ViewBag.phase = phases;
                    return View(insertProjectPhaseDTO);
                }
                var projectphases = new ProjectPhase()
                {
                    Id = Guid.NewGuid(),
                    ProjectId = insertProjectPhaseDTO.ProjectId,
                    EndDate = insertProjectPhaseDTO.EndDate,
                    PhaseId = insertProjectPhaseDTO.PhaseId,
                    StartDate = insertProjectPhaseDTO.StartDate
                };
                _projectPhaseRepository.Insert(projectphases);
                _projectPhaseRepository.Save();
                TempData["save"] = "Phases has been Created Successfully ...";
                return RedirectToAction("Index", new { id = insertProjectPhaseDTO.ProjectId });

            }
            else
            {
                return View("Create");
            }
        }

        // GET: ProjectManagment/ProjectPhase/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var projectPhase = await _projectPhaseRepository.GetProjectPhaseById(id);
            ViewBag.projectPhase = projectPhase;
            var phases = await _phaseRepository.GetAllPhases();
            ViewBag.phase = phases;
            //var project = await _projectRepository.GetProjectById(projectPhase.ProjectId);
            //ViewBag.project = project;
            var pPhase = await _projectPhaseRepository.GetAllProjectPhases();
            ViewBag.projectPahse = pPhase;
            ViewData["PhaseId"] = new SelectList(phases, "Id", "PhaseName", projectPhase.PhaseId);
            if (projectPhase == null)
            {
                return NotFound();
            }
           

            return View();
        }

        // POST: ProjectManagment/ProjectPhase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProjectPhasesDTO updateProjectPhasesDTO)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var projectPhases = new ProjectPhase()
                    {
                        Id=updateProjectPhasesDTO.ProjectPhaseId,
                        EndDate =updateProjectPhasesDTO.EndDate,
                        PhaseId=updateProjectPhasesDTO.PhaseId,
                        ProjectId = updateProjectPhasesDTO.ProjectId,
                        StartDate=updateProjectPhasesDTO.StartDate
                    };
                    var item = _projectPhaseRepository.IsPhaseExistForUpdate(projectPhases.ProjectId,projectPhases.PhaseId);
                    if (item)
                    {
                        ModelState.AddModelError("", "Sorry you have been used this Phase already in the project");
                        var projectPhase = await _projectPhaseRepository.GetProjectPhaseById(updateProjectPhasesDTO.ProjectPhaseId);
                        ViewBag.projectPhase = projectPhase;
                        var phases = await _phaseRepository.GetAllPhases();
                        ViewBag.phase = phases;
                        //var project = await _projectRepository.GetProjectById(projectPhase.ProjectId);
                        //ViewBag.project = project;
                        var pPhase = await _projectPhaseRepository.GetAllProjectPhases();
                        ViewBag.projectPahse = pPhase;
                        ViewData["PhaseId"] = new SelectList(phases, "Id", "PhaseName", projectPhase.PhaseId);
                        return View(updateProjectPhasesDTO);
                    }
                    _projectPhaseRepository.Update(projectPhases);
                    _projectPhaseRepository.Save();
                    //ViewBag.project = await _projectRepository.GetProjectById(projectPhase.ProjectId);
                    TempData["edit"] = "Phases has been Updated Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (updateProjectPhasesDTO.ProjectPhaseId==null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),new { id=updateProjectPhasesDTO.ProjectId});
            }
            return View(updateProjectPhasesDTO);
        }

        // GET: ProjectManagment/ProjectPhase/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectPhase = await _projectPhaseRepository.GetProjectPhaseById(id);
            if (projectPhase == null)
            {
                return NotFound();
            }

            return View(projectPhase);
        }

        // POST: ProjectManagment/ProjectPhase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            
            var projectPhase = await _projectPhaseRepository.GetProjectPhaseById(id);
            _projectPhaseRepository.Delete(projectPhase);
            _projectPhaseRepository.Save();
            TempData["delete"] = "Phases has been Deleted Successfully ...";
            ViewBag.project = await _projectRepository.GetProjectById(projectPhase.ProjectId);
            return RedirectToAction(nameof(Index),new { id=projectPhase.ProjectId});
        }

    }
}
