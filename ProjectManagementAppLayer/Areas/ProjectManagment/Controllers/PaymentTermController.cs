using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<Person> _userManager;
        public PaymentTermController(
            ApplicationDbContext context,
            IPaymentTermRepository paymentTermRepository,
            IDeliverableRepository deliverableRepository
, UserManager<Person> userManager)
        {
            _context = context;
            _paymentTermRepository = paymentTermRepository;
            _deliverableRepository = deliverableRepository;
            _userManager = userManager;
        }

        // GET: ProjectManagment/PaymentTerm
        public async Task<IActionResult> Index()
        {
            var res = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Admin");
            if (res)
            {
                var item1 = await _paymentTermRepository.GetAllPaymentTerms();
                return View(item1);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var item2 = await _paymentTermRepository.GetAllPaymentTermByProjectManagerId(userId);
            return View(item2);
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
            var ty = await _paymentTermRepository.GetIsNotPaidPaymentTerm(id);
            return new JsonResult(ty);
        }


        //get specific paymentterm based on the id 
        //ProjectManagment/PaymentTerm/GetPaymentSum?id=CB41A9FA-AB5B-42E0-ACB5-C28B58FA74CC
        // return paymentTerm that is not paid 
        public async Task<JsonResult> GetPaymentSum(Guid id)
        {
            var ty = await _context.PaymentTerms
                .Include(q => q.Deliverable)
                .Include(r => r.Deliverable.ProjectPhase)
                .Include(v => v.Deliverable.ProjectPhase.Project)
                .Include(v => v.Deliverable.ProjectPhase.Phase)
                .SingleOrDefaultAsync(t=>t.Id==id);

            if (ty.IsPaid == true)
            {
                return new JsonResult(new { message = "Sorry This paymentTerm is already paid" }) ;
            }
            return new JsonResult(ty);
        }
        // ProjectManagment/PaymentTerm/GetPaymentTerm?id=CB41A9FA-AB5B-42E0-ACB5-C28B58FA74CC
        //public async Task<JsonResult>GetPaymentTerm(Guid id)
        //{
        //    if(id == null)
        //    {
        //        return new JsonResult(new { message = "Sorry This paymentTerm is not found" });

        //    }
        //    var pay = await _paymentTermRepository.GetIsNotPaidPaymentTerm(id);
        //    if (pay.IsPaid == true)
        //    {
        //        return new JsonResult(new { message = "Sorry This paymentTerm is already paid" });
        //    }
        //    return new JsonResult(pay);
        //}
    }
}
