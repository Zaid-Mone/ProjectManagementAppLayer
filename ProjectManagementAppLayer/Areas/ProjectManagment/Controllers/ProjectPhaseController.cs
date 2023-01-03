using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAppLayer.DTOs.Insert;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;

namespace ProjectManagementAppLayer.Areas.ProjectManagment.Controllers
{
    [Area("ProjectManagment")]
    public class ProjectPhaseController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IPhaseRepository _phaseRepository;
        public ProjectPhaseController(ApplicationDbContext context, IProjectRepository projectRepository, IProjectPhaseRepository projectPhaseRepository, IPhaseRepository phaseRepository)
        {

            _context = context;
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InsertProjectPhaseDTO insertProjectPhaseDTO)
        {
            if (ModelState.IsValid)
            {
                var projectphases = new ProjectPhase()
                {
                    Id = Guid.NewGuid(),
                    ProjectId=insertProjectPhaseDTO.ProjectId,
                    EndDate=insertProjectPhaseDTO.EndDate,
                    PhaseId= insertProjectPhaseDTO.PhaseId,
                    StartDate=insertProjectPhaseDTO.StartDate
                };
                _projectPhaseRepository.Insert(projectphases);
                _projectPhaseRepository.Save();
                TempData["save"] = "Phases has been Created Successfully ...";
                return RedirectToAction("Index",new { id= insertProjectPhaseDTO.ProjectId});
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
            var project = await _projectRepository.GetProjectById(projectPhase.ProjectId);
            ViewBag.project = project;


            if (projectPhase == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: ProjectManagment/ProjectPhase/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProjectPhase projectPhase)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _projectPhaseRepository.Update(projectPhase);
                    _projectPhaseRepository.Save();
                    ViewBag.project = await _projectRepository.GetProjectById(projectPhase.ProjectId);
                    TempData["edit"] = "Phases has been Updated Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectPhaseExists(projectPhase.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index),new { id=projectPhase.ProjectId});
            }
            ViewData["PhaseId"] = new SelectList(_context.Phases, "Id", "Id", projectPhase.PhaseId);
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", projectPhase.ProjectId);
            return View(projectPhase);
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

        private bool ProjectPhaseExists(Guid id)
        {
            return _context.ProjectPhases.Any(e => e.Id == id);
        }
    }
}
