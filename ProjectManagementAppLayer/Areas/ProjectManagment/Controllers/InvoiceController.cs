using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagementAppLayer.DTOs.Insert;
using ProjectManagementAppLayer.DTOs.Update;
using ProjectManagementBusinessLayer.Data;
using ProjectManagementBusinessLayer.Entities;
using ProjectManagementBusinessLayer.Repositories.Implementation;
using ProjectManagementBusinessLayer.Repositories.Interfaces;

namespace ProjectManagementAppLayer.Areas.ProjectManagment.Controllers
{
    [Area("ProjectManagment")]
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IInvoicePaymentTermsRepository _invoicePaymentTerms;
        private readonly IPaymentTermRepository _paymentTermRepository;
        private readonly UserManager<Person> _userManager;

        public InvoiceController(ApplicationDbContext context,
            IInvoiceRepository invoiceRepository,
            IProjectRepository projectRepository,
           IInvoicePaymentTermsRepository invoicePaymentTerms,
           IPaymentTermRepository paymentTermRepository, UserManager<Person> userManager)
        {
            _context = context;
            _invoiceRepository = invoiceRepository;
            _projectRepository = projectRepository;
            _invoicePaymentTerms = invoicePaymentTerms;
            _paymentTermRepository = paymentTermRepository;
            _userManager = userManager;
        }

        // GET: ProjectManagment/Invoice
        public async Task<IActionResult> Index()
        {
            var res = await _userManager.IsInRoleAsync(await _userManager.GetUserAsync(User), "Admin");
            if (res)
            {
                var item1 = await _invoiceRepository.GetAllInvoices();
                return View(item1);
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var item = await _invoiceRepository.GetAllInvoicesByProjectManagerId(userId);
            return View(item);
        }

        // GET: ProjectManagment/Invoice/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if(User.Identity.IsAuthenticated)
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceRepository.GetInvoiceById(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // GET: ProjectManagment/Invoice/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.projcet = await _projectRepository.GetAllProjects();
            return View();
        }

        // POST: ProjectManagment/Invoice/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsertInvoiceDTO insertInvoiceDTO)
        {
            if (ModelState.IsValid)
            {
                var invoice = new Invoice()
                {
                     InvoiceDate= insertInvoiceDTO.InvoiceDate,
                     InvoiceTitle=insertInvoiceDTO.InvoiceTitle,
                     ProjectId=insertInvoiceDTO.ProjectId,
                     
                };
                _invoiceRepository.Insert(invoice);
                _invoiceRepository.Save();

                foreach (var item in insertInvoiceDTO.PaymentTermIds)
                {
                    
                    var invoicePayment = new InvoicePaymentTerms();

                    invoicePayment.InvoiceId = invoice.Id;
                    invoicePayment.PaymentTermId = item;

                    // not working to set isPaid = true; object refernec =null
                    var pTerm = await _paymentTermRepository.GetPaymentTermById(item);
                    if (pTerm != null)
                    {
                        pTerm.IsPaid = true;
                    }
                    _invoicePaymentTerms.Insert(invoicePayment);
                }
                _invoicePaymentTerms.Save();
                TempData["save"] = "Invoice has been Created Successfully ...";
                return RedirectToAction(nameof(Index));
            }
            return View(insertInvoiceDTO);
        }

        // GET: ProjectManagment/Invoice/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
           
            var invoice = await _invoiceRepository.GetInvoiceById(id);
            ViewBag.invoice = invoice;
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.pay =  _paymentTermRepository.GetProjectPaymentTerms(invoice.ProjectId);
            
            if (invoice == null)
            {
                return NotFound();
            }
            return View();
        }

        // POST: ProjectManagment/Invoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateInvoiceDTO updateInvoiceDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var invo = await _invoiceRepository.GetInvoiceById(updateInvoiceDTO.InvoiceId);
                    invo.InvoiceDate = updateInvoiceDTO.InvoiceDate;
                    invo.InvoiceTitle = updateInvoiceDTO.InvoiceTitle;
                    foreach (var item in invo.InvoicePaymentTerms)
                    {
                        var pTerm = await _paymentTermRepository.GetPaymentTermById(item.PaymentTermId);
                        if (pTerm != null)
                        {
                            pTerm.IsPaid = false;
                        }
                        _invoicePaymentTerms.Delete(item);

                    }
                    foreach (var item2 in updateInvoiceDTO.InvoicePaymentsIds)
                    {
                        
                        var pays = new InvoicePaymentTerms()
                        {
                            InvoiceId = updateInvoiceDTO.InvoiceId,
                            PaymentTermId = item2
                        };
                        var pTerm = await _paymentTermRepository.GetPaymentTermById(item2);
                        if (pTerm != null)
                        {
                            pTerm.IsPaid = true;
                        }
                        _invoicePaymentTerms.Insert(pays);
                    }
                    _invoicePaymentTerms.Save();
                    TempData["edit"] = "Invoice has been Updated Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (updateInvoiceDTO.InvoiceId ==null)
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
            return View(updateInvoiceDTO);
        }

        // GET: ProjectManagment/Invoice/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _invoiceRepository.GetInvoiceById(id);
            if (invoice == null)
            {
                return NotFound();
            }

            return View(invoice);
        }

        // POST: ProjectManagment/Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var invoice = await _invoiceRepository.GetInvoiceById(id);
            foreach (var item in invoice.InvoicePaymentTerms)
            {
                var pTerm = await _paymentTermRepository.GetPaymentTermById(item.PaymentTermId);
                if (pTerm != null)
                {
                    pTerm.IsPaid = false;
                }
                _invoicePaymentTerms.Delete(item);
            }
            _invoiceRepository.Delete(invoice);
            _invoicePaymentTerms.Save();
            TempData["delete"] = "Invoice has been Deleted Successfully ...";
            return RedirectToAction(nameof(Index));
        }




        public IActionResult Print()
        {
            return View();
        }

        // /ProjectManagment/Invoice/GetPayments?id=FB950297-1BDE-41D6-989E-08DAEF5A4FCE
        // /ProjectManagment/Invoice/GetPaymentsForInvoices?id=EA2BF9AE-A019-4B79-4F9B-08DAEE916C39
        public async Task<JsonResult> GetPaymentsForInvoices(Guid id)
        {
            var res1 = await _invoiceRepository.GetInvoiceById(id);
            //var res2 = await _invoicePaymentTerms.GetInvoicePaymentTermByIdByInvoiceId(res1.Id);

            return new JsonResult(res1);
        }

    }
}
