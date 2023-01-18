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
        private readonly IInvoiceRepository _invoiceRepository ;
        private readonly IInvoicePaymentTermsRepository _invoicePaymentTermsRepository ;
        public PaymentTermController(
            ApplicationDbContext context,
            IPaymentTermRepository paymentTermRepository,
            IDeliverableRepository deliverableRepository,
           UserManager<Person> userManager,
            IInvoiceRepository invoiceRepository, IInvoicePaymentTermsRepository invoicePaymentTermsRepository)
        {
            _context = context;
            _paymentTermRepository = paymentTermRepository;
            _deliverableRepository = deliverableRepository;
            _userManager = userManager;
            _invoiceRepository = invoiceRepository;
            _invoicePaymentTermsRepository = invoicePaymentTermsRepository;
        }

        // GET: ProjectManagment/PaymentTerm
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var res = await (_userManager.IsInRoleAsync(user, "Admin")) ||
                await (_userManager.IsInRoleAsync(user, "ProjectDirector"));
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
        public async Task<IActionResult> Create(PaymentTerm paymentTerm)
        {
            if (ModelState.IsValid)
            {
 
                var result = await _deliverableRepository.GetDeliverableById(paymentTerm.DeliverableId);
                var pay = _paymentTermRepository.GetProjectPaymentTerms(result.ProjectPhase.ProjectId);
                    decimal sum = 0;
                    paymentTerm.Id = Guid.NewGuid();
                    foreach (var item in pay)
                    {
                        sum += item.PaymentTermAmount;
                    }
                    sum += paymentTerm.PaymentTermAmount;
                    if (sum > result.ProjectPhase.Project.ContractAmount)
                    {
                        ViewBag.msg = false;
                        ModelState.AddModelError("", $"The Total Project Amount is {result.ProjectPhase.Project.ContractAmount.ToString("C")} You can't Add More than this..");
                        await Create(paymentTerm.DeliverableId);
                        return View(paymentTerm);
                    }
                    _paymentTermRepository.Insert(paymentTerm);
                    _paymentTermRepository.Save();
                    TempData["save"] = "PaymentTerm has been Created Successfully ...";
                    return RedirectToAction(nameof(Index));
                
                //ModelState.AddModelError("", $"The Start Date must be bigger Than or equal {result.ProjectPhase.Project.StartDate.Date} && The End Date must be less Than or equal {result.ProjectPhase.Project.EndDate.Date}");
                //ModelState.AddModelError("", $"The Amount of PaymentTerm mustn't be bigger than Project Amount {result.ProjectPhase.Project.ContractAmount.ToString("C")}");
                //await Create(paymentTerm.DeliverableId);
                //return View(paymentTerm);
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
        public async Task<IActionResult> Edit(UpdatePaymentTermDTO updatePaymentTermDTO)
        {
            if (ModelState.IsValid)
            {
                decimal sum=0;
                var result = await _deliverableRepository.GetDeliverableById(updatePaymentTermDTO.DeliverableId);
                var pay = _paymentTermRepository.GetProjectPaymentTerms(result.ProjectPhase.ProjectId);

                if (updatePaymentTermDTO.PaymentTermId == null) return NotFound();
                try
                {
                    var payment = await _paymentTermRepository.GetPaymentTermById(updatePaymentTermDTO.PaymentTermId);
                    payment.PaymentTermTitle = updatePaymentTermDTO.PaymentTermTitle;
                    payment.PaymentTermAmount = updatePaymentTermDTO.PaymentTermAmount;
                    foreach (var item in pay)
                    {
                        sum += item.PaymentTermAmount;
                    }
                    sum += updatePaymentTermDTO.PaymentTermAmount;
                    if (sum > result.ProjectPhase.Project.ContractAmount)
                    {
                        ModelState.AddModelError("", $"The Total Project Amoutn is {result.ProjectPhase.Project.ContractAmount.ToString("C")} You can't Add More than this..");

                        await Edit(updatePaymentTermDTO.PaymentTermId);
                        return View(updatePaymentTermDTO);
                    }
                    _paymentTermRepository.Update(payment);
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


        //ProjectManagment/PaymentTerm/UpdateInvoicPaymentTerm?id=EA2BF9AE-A019-4B79-4F9B-08DAEE916C39
        public async Task<JsonResult> UpdateInvoicPaymentTerm(Guid id)
        {
           var payments = await _paymentTermRepository.GetAllPaymentTerms();
           var invoicesPayment = await _invoicePaymentTermsRepository.GetInvoicePaymentTermByIdByProjectId(id);
            
           //var findUnmatched = from pt in payments
           //                     where (from ptI in invoicesPayment select ptI.PaymentTermId )
           //                    .Contains(pt.Id)
           //                     select pt;
            return new JsonResult(invoicesPayment);
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
