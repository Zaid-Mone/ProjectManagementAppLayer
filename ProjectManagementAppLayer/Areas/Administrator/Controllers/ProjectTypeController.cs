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
    [Authorize(Roles ="Admin")]
    public class ProjectTypeController : Controller
    {
        
        private readonly IProjectTypeRepository _projectTypeRepository;
        public ProjectTypeController(
            IProjectTypeRepository projectTypeRepository)
        {
      
            _projectTypeRepository = projectTypeRepository;
        }

        // GET: Administrator/ProjectType
        public async Task<IActionResult> Index()
        {
            return View(await _projectTypeRepository.GetAllProjectTypes());
        }

        // GET: Administrator/ProjectType/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _projectTypeRepository.GetProjectTypeById(id);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // GET: Administrator/ProjectType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/ProjectType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProjectType projectType)
        {
            if (ModelState.IsValid)
            {
                projectType.Id = Guid.NewGuid();
                _projectTypeRepository.Insert(projectType);
                _projectTypeRepository.Save();
                TempData["save"] = "ProjectType has been Created Successfully ...";
                return RedirectToAction(nameof(Index));
            }
            return View(projectType);
        }

        // GET: Administrator/ProjectType/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _projectTypeRepository.GetProjectTypeById(id);
            if (projectType == null)
            {
                return NotFound();
            }
            return View(projectType);
        }

        // POST: Administrator/ProjectType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProjectType projectType)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _projectTypeRepository.Update(projectType);
                    _projectTypeRepository.Save();
                     TempData["edit"] = "ProjectType has been Updated Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_projectTypeRepository.ProjectTypeExists(projectType.Id))
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
            return View(projectType);
        }

        // GET: Administrator/ProjectType/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectType = await _projectTypeRepository.GetProjectTypeById(id);
            if (projectType == null)
            {
                return NotFound();
            }

            return View(projectType);
        }

        // POST: Administrator/ProjectType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var projectType = await _projectTypeRepository.GetProjectTypeById(id);
            _projectTypeRepository.Delete(projectType);
            _projectTypeRepository.Save();
            TempData["delete"] = "ProjectType has been Deleted Successfully ...";
            return RedirectToAction(nameof(Index));
        }

        
    }
}
