using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;

namespace ProjectManagementAppLayer.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize(Roles = "Admin")]
    public class ProjectStatusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IProjectStatusRepository _projectStatusRepository;
        public ProjectStatusController(ApplicationDbContext context, 
            IProjectStatusRepository projectStatusRepository)
        {
            _context = context;
            _projectStatusRepository = projectStatusRepository;
        }

        // GET: Administrator/ProjectStatus
        public async Task<IActionResult> Index()
        {
            return View(await _projectStatusRepository.GetAllProjectStatuses());
        }

        // GET: Administrator/ProjectStatus/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectStatus = await _projectStatusRepository.GetProjectStatusById(id);
            if (projectStatus == null)
            {
                return NotFound();
            }

            return View(projectStatus);
        }

        // GET: Administrator/ProjectStatus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/ProjectStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectStatus projectStatus)
        {
            if (ModelState.IsValid)
            {
                projectStatus.Id = Guid.NewGuid();
                _projectStatusRepository.Insert(projectStatus);
                _projectStatusRepository.Save();
                TempData["save"] = "ProjectStatus has been Created Successfully ...";
                return RedirectToAction(nameof(Index));
            }
            return View(projectStatus);
        }

        // GET: Administrator/ProjectStatus/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectStatus = await _projectStatusRepository.GetProjectStatusById(id);
            if (projectStatus == null)
            {
                return NotFound();
            }
            return View(projectStatus);
        }

        // POST: Administrator/ProjectStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( ProjectStatus projectStatus)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _projectStatusRepository.Update(projectStatus);
                    _projectStatusRepository.Save();
                    TempData["edit"] = "ProjectStatus has been Updated Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_projectStatusRepository.ProjectStatusExists(projectStatus.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projectStatus);
        }

        // GET: Administrator/ProjectStatus/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectStatus = await _projectStatusRepository.GetProjectStatusById(id);
            if (projectStatus == null)
            {
                return NotFound();
            }

            return View(projectStatus);
        }

        // POST: Administrator/ProjectStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var projectStatus = await _projectStatusRepository.GetProjectStatusById(id);
            _projectStatusRepository.Delete(projectStatus);
            _projectStatusRepository.Save();
            TempData["delete"] = "ProjectStatus has been Deleted Successfully ...";
            return RedirectToAction(nameof(Index));
        }

    }
}
