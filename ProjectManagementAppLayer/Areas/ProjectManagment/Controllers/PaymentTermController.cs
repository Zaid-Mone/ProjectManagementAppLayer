using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAppLayer.DTOs.Update;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Interfaces;

namespace ProjectManagementAppLayer.Areas.ProjectManagment.Controllers
{
    [Area("ProjectManagment")]
    public class PaymentTermController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPaymentTermRepository _paymentTermRepository;
        private readonly IDeliverableRepository _deliverableRepository;

        public PaymentTermController(
            ApplicationDbContext context,
            IPaymentTermRepository paymentTermRepository, 
            IDeliverableRepository deliverableRepository
            )
        {
            _context = context;
            _paymentTermRepository = paymentTermRepository;
            _deliverableRepository = deliverableRepository;
        }

        // GET: ProjectManagment/PaymentTerm
        public async Task<IActionResult> Index()
        {
            var item = await _paymentTermRepository.GetAllPaymentTerms();
            return View(item);
        }

        // GET: ProjectManagment/PaymentTerm/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentTerm = await _paymentTermRepository.GetPaymentTermById(id);
            if (paymentTerm == null)
            {
                return NotFound();
            }

            return View(paymentTerm);
        }

        // GET: ProjectManagment/PaymentTerm/Create
        public async Task<IActionResult> Create(Guid id)
        {
            var delv = await _deliverableRepository.GetDeliverableById(id);
            ViewBag.deliv = delv;
            return View();
        }

        // POST: ProjectManagment/PaymentTerm/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(PaymentTerm paymentTerm)
        {
            if (ModelState.IsValid)
            {
                paymentTerm.Id = Guid.NewGuid();
                _paymentTermRepository.Insert(paymentTerm);
                _paymentTermRepository.Save();
                TempData["save"] = "PaymentTerm has been Created Successfully ...";
                return RedirectToAction(nameof(Index));
            }
            return View(paymentTerm);
        }

        // GET: ProjectManagment/PaymentTerm/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var paymentTerm = await _paymentTermRepository.GetPaymentTermById(id);
            if (paymentTerm == null)
            {
                return NotFound();
            }
            ViewBag.pTerm = paymentTerm;
            return View();
        }

        // POST: ProjectManagment/PaymentTerm/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdatePaymentTermDTO updatePaymentTermDTO)
        {
            if (ModelState.IsValid)
            {
                if (updatePaymentTermDTO.PaymentTermId == null) return NotFound();
                try
                {
                    var obj = new PaymentTerm() { 
                    Id = updatePaymentTermDTO.PaymentTermId,
                     DeliverableId=updatePaymentTermDTO.DeliverableId,
                     PaymentTermAmount=updatePaymentTermDTO.PaymentTermAmount,
                     PaymentTermTitle=updatePaymentTermDTO.PaymentTermTitle
                    };
                    _paymentTermRepository.Update(obj);
                    _paymentTermRepository.Save();
                    TempData["edit"] = "PaymentTerm has been Created Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (updatePaymentTermDTO.PaymentTermId==null)
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
            return View(updatePaymentTermDTO);
        }

        // GET: ProjectManagment/PaymentTerm/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var paymentTerm = await _paymentTermRepository.GetPaymentTermById(id);
            if (paymentTerm == null)
            {
                return NotFound();
            }

            return View(paymentTerm);
        }

        // POST: ProjectManagment/PaymentTerm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var paymentTerm = await _paymentTermRepository.GetPaymentTermById(id);
            _paymentTermRepository.Delete(paymentTerm);
            _paymentTermRepository.Save();
            TempData["delete"] = "PaymentTerm has been Deleted Successfully ...";
            return RedirectToAction(nameof(Index));
        }




        public async Task<JsonResult> GetprojectPayments(Guid id)
        {
            var ty = await _context.PaymentTerms
                .Include(q => q.Deliverable)
                .Include(r => r.Deliverable.ProjectPhase)
                .Include(v => v.Deliverable.ProjectPhase.Project)
                .Include(v => v.Deliverable.ProjectPhase.Phase)
                .Where(z => z.Deliverable.ProjectPhase.Project.Id == id)
                .ToListAsync();
            return new JsonResult(ty);
        }
    }
}
