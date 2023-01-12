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
    [Authorize(Roles="Admin")]
    public class ClientController : Controller
    {
        
        private readonly IClientRepository _clientRepository;
        public ClientController(IClientRepository clientRepository)
        {
         
            this._clientRepository = clientRepository;
        }

        // GET: Administrator/Client
        public async Task<IActionResult> Index()
        {
            return View(await _clientRepository.GetAllClients());
        }

        // GET: Administrator/Client/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Administrator/Client/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Client/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client client)
        {
            if (ModelState.IsValid)
            {
                client.Id = Guid.NewGuid();
                _clientRepository.Insert(client);
                _clientRepository.Save();
                TempData["save"] = "Client has been Created Successfully ...";
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: Administrator/Client/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: Administrator/Client/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Client client)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _clientRepository.Update(client);
                    _clientRepository.Save();
                    TempData["edit"] = "Client has been Updated Successfully ...";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_clientRepository.ClientExists(client.Id))
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
            return View(client);
        }

        // GET: Administrator/Client/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _clientRepository.GetClientById(id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Administrator/Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var client = await _clientRepository.GetClientById(id);
            _clientRepository.Delete(client);
            _clientRepository.Save();
            TempData["delete"] = "Client has been Deleted Successfully ...";
            return RedirectToAction(nameof(Index));
        }

        
    }
}
