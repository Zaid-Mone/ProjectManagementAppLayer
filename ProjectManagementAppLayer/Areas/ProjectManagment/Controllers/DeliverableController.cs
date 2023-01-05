using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;

namespace ProjectManagementAppLayer.Areas.ProjectManagment.Controllers
{
    [Area("ProjectManagment")]
    public class DeliverableController : Controller
    {
     
        private readonly IDeliverableRepository _deliverableRepository;
        private readonly IProjectPhaseRepository _projectPhaseRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly UserManager<Person> _userManager;

        public DeliverableController(IDeliverableRepository deliverableRepositor,
            IProjectPhaseRepository projectPhaseRepository,
            IProjectRepository projectRepository, UserManager<Person> userManager)
        {

            _deliverableRepository = deliverableRepositor;
            _projectPhaseRepository = projectPhaseRepository;
            _projectRepository = projectRepository;
            _userManager = userManager;
        }

        // GET: ProjectManagment/Deliverable
        public async Task<IActionResult> Index()
        {
            var res = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Admin");
            if (res)
            {
                var item1 = await _deliverableRepository.GetAllDeliverables();
                return View(item1);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var item = await _deliverableRepository.GetAllDeliverableForProjectManager(userId);
            return View(item);
        }

        // GET: ProjectManagment/Deliverable/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliverable = await _deliverableRepository.GetDeliverableById(id);
            if (deliverable == null)
            {
                return NotFound();
            }

            return View(deliverable);
        }

        // GET: ProjectManagment/Deliverable/Create
        public async Task<IActionResult> Create(Guid id)
        {
            var projectPhases = await _projectPhaseRepository.GetProjectPhaseById(id);
            ViewBag.projectphase = projectPhases;

            var projects = await _projectRepository.GetProjectById(projectPhases.Id);
            ViewBag.project = projects;
            return View();
        }

        // POST: ProjectManagment/Deliverable/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Deliverable deliverable)
        {
            if (ModelState.IsValid)
            {
                deliverable.Id = Guid.NewGuid();
                _deliverableRepository.Insert(deliverable);
                _deliverableRepository.Save();
                TempData["save"] = "Deliverable has been Created Successfully ...";
                return RedirectToAction(nameof(Index));
            }
            var projectPhases = await _projectPhaseRepository.GetProjectPhaseById(deliverable.ProjectPhaseId);
            ViewBag.projectphase = projectPhases;

            var projects = await _projectRepository.GetProjectById(projectPhases.Id);
            ViewBag.project = projects;
            return View(deliverable);
        }

        // GET: ProjectManagment/Deliverable/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deliverable = await _deliverableRepository.GetDeliverableById(id);
            if (deliverable == null)
            {
                return NotFound();
            }
            return View(deliverable);
        }

        // POST: ProjectManagment/Deliverable/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( Deliverable deliverable)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _deliverableRepository.Update(deliverable);
                    _deliverableRepository.Save();
                    TempData["edit"] = "Deliverable has been Updated Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                        if(deliverable.Id == null)
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(deliverable);
        }

        // GET: ProjectManagment/Deliverable/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliverable = await _deliverableRepository.GetDeliverableById(id);
            if (deliverable == null)
            {
                return NotFound();
            }

            return View(deliverable);
        }

        // POST: ProjectManagment/Deliverable/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var deliverable = await _deliverableRepository.GetDeliverableById(id);
            _deliverableRepository.Delete(deliverable);
            _deliverableRepository.Save();
            TempData["delete"] = "Deliverable has been Deleted Successfully ...";
            return RedirectToAction(nameof(Index));
        }


    }
}
