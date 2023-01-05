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
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices.FindAsync(id);
            if (invoice == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", invoice.ProjectId);
            return View(invoice);
        }

        // POST: ProjectManagment/Invoice/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(invoice);
                    await _context.SaveChangesAsync();
                    TempData["edit"] = "Invoice has been Updated Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (invoice.Id ==null)
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
            ViewData["ProjectId"] = new SelectList(_context.Projects, "Id", "Id", invoice.ProjectId);
            return View(invoice);
        }

        // GET: ProjectManagment/Invoice/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var invoice = await _context.Invoices
                .Include(i => i.Project)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var invoice = await _context.Invoices.FindAsync(id);
            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();
            TempData["delete"] = "Invoice has been Deleted Successfully ...";
            return RedirectToAction(nameof(Index));
        }


    }
}
