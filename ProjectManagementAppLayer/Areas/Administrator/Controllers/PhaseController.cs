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
    public class PhaseController : Controller
    {
       
        private readonly IPhaseRepository _phaseRepository;
      
        public PhaseController( IPhaseRepository phaseRepository)
        {
            _phaseRepository = phaseRepository;
        }

        // GET: Administrator/Phase
        public async Task<IActionResult> Index()
        {
            return View(await _phaseRepository.GetAllPhases());
        }

        // GET: Administrator/Phase/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phase = await _phaseRepository.GetPhaseById(id);
            if (phase == null)
            {
                return NotFound();
            }

            return View(phase);
        }

        // GET: Administrator/Phase/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Phase/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Phase phase)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    phase.Id = Guid.NewGuid();
                    _phaseRepository.Insert(phase);
                    _phaseRepository.Save();
                    TempData["save"] = "Phases has been Created Successfully ...";
                    return RedirectToAction(nameof(Index));
                }
                return View(phase);

            }
            catch
            {
                return View();
            }
        }

        // GET: Administrator/Phase/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phase = await _phaseRepository.GetPhaseById(id);
            if (phase == null)
            {
                return NotFound();
            }
            return View(phase);
        }

        // POST: Administrator/Phase/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Phase phase)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _phaseRepository.Update(phase);
                    _phaseRepository.Save();
                    TempData["edit"] = "Phases has been Updated Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_phaseRepository.PhaseExists(phase.Id))
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
            return View(phase);
        }

        // GET: Administrator/Phase/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var phase = await _phaseRepository.GetPhaseById(id);
            if (phase == null)
            {
                return NotFound();
            }

            return View(phase);
        }

        // POST: Administrator/Phase/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var phase = await _phaseRepository.GetPhaseById(id);
            _phaseRepository.Delete(phase);
            _phaseRepository.Save();
            TempData["delete"] = "Phases has been Deleted Successfully ...";
            return RedirectToAction(nameof(Index));
        }

    }
}
