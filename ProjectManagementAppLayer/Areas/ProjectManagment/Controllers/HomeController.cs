using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProjectManagementAppLayer.Models;
using ProjectManagementBusinessLayer.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ProjectManagementAppLayer.Areas.ProjectManagment.Controllers
{
    [Area("ProjectManagment")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            this._db = db;
        }

        public IActionResult Index()
        {

            if (User.IsInRole("Admin"))
            {
                IsAdmin();
                return View();
            }
            else if (User.IsInRole("ProjectDirector"))
            {
                IsProjectDirector();

                return View();
            }
            else
            {
                IsProjectManager();
                BarChart();
                return View();
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Chatting()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _db.Users.FirstOrDefault(q => q.Id == userId);
            //user.ImageUrl
            //user.FullName
            ViewBag.user = user;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public void IsAdmin()
        {
            ViewBag.projectCount = _db.Projects
                .Include(a => a.ProjectStatus)
                .Include(t => t.ProjectType)
                .ToList().Count;


            // for count all user payements
            var res = _db.PaymentTerms
                .Include(q => q.Deliverable)
                .ThenInclude(r => r.ProjectPhase)
                .ThenInclude(v => v.Project)
                .ToList();
            decimal sum = 0;
            foreach (var item in res)
            {
                sum += item.PaymentTermAmount;
            }
            ViewBag.sumation = sum.ToString("C");

            var pstatus = _db.ProjectStatuses.ToList();

            // number of users
            var users =  _db.Users.ToList().Count;
            ViewBag.users = users;

            // get count of pending projects
            var res1 = _db.Projects
               .Include(e => e.ProjectStatus)
               .Where(y => y.ProjectStatus.Status == "Pending").ToList().Count;

            ViewBag.status = res1;

            // get count of active projects
            var res2 = _db.Projects
               .Include(e => e.ProjectStatus)
               .Where(y => y.ProjectStatus.Status == "Active").ToList().Count;

            ViewBag.active = res2;




        }
        public void IsProjectManager()
        {
            var usr = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewBag.projectCount = _db.Projects
                .Where(e => e.ProjectManagerId == usr)
                .Include(a => a.ProjectStatus)
                .Include(t => t.ProjectType)
                .ToList().Count;


            // for count all user payements
            var res = _db.PaymentTerms
                .Include(q => q.Deliverable)
                .ThenInclude(r => r.ProjectPhase)
                .ThenInclude(v => v.Project)
                .Where(z => z.Deliverable.ProjectPhase.Project.ProjectManagerId == usr)
                .ToList();
            decimal sum = 0;
            foreach (var item in res)
            {
                sum += item.PaymentTermAmount;
            }
            ViewBag.sumation = sum.ToString("C");

            var pstatus = _db.ProjectStatuses.ToList();

            // get count of pending projects
            var res1 = _db.Projects
               .Include(e => e.ProjectStatus)
               .Where(t => t.ProjectManagerId == usr)
               .Where(y => y.ProjectStatus.Status == "Pending").ToList().Count;

            ViewBag.status = res1;

            // get count of active projects
            var res2 = _db.Projects
               .Include(e => e.ProjectStatus)
               .Where(t => t.ProjectManagerId == usr)
               .Where(y => y.ProjectStatus.Status == "Active").ToList().Count;

            ViewBag.active = res2;


        }
        public void IsProjectDirector()
        {
            ViewBag.PendingProject = _db.Projects
                .Where(e => e.IsApproved==false)
                .Include(a => a.ProjectStatus)
                .Include(t => t.ProjectType)
                .ToList().Count;
            ViewBag.ApprovedProject = _db.Projects
                .Where(e => e.IsApproved == true)
                .Include(a => a.ProjectStatus)
                .Include(t => t.ProjectType)
                .ToList().Count;    
            ViewBag.PendingInvoice = _db.Invoices // pending in invoice mean you can edit the invoice
                .Where(e => e.IsApproved == false)
                .Include(a => a.Project)
                .ToList().Count;        
            ViewBag.ApprovedInvoice = _db.Invoices // Approved in invoice mean you can't edit the invoice
                .Where(e => e.IsApproved == true)
                .Include(a => a.Project)
                .ToList().Count;

        }
        public void BarChart()
        {
            var usr = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // cost // project name
            Dictionary<decimal, string> barchart = new Dictionary<decimal, string>();

            var project = _db.Projects
                    .Where(e => e.ProjectManagerId == usr)
                    .Include(a => a.ProjectStatus)
                    .Include(t => t.ProjectType)
                    .ToList();

            foreach (var item in project)
            {
                barchart.Add(item.ContractAmount, item.ProjectName);
            }

            ViewBag.dict = barchart;
        }
    }
}
