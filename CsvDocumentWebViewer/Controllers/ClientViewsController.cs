using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CsvDocumentWebViewer.Services.Models;
using CsvDocumentWebViewer.Services.ViewsRepository.ClientViewRepo;

namespace CsvDocumentWebViewer.Controllers
{
    public class ClientViewsController : Controller
    {
        private readonly CsvDocumentWebViewerContext _context;
        private readonly IClientViewRepository _clientViewRepository;

        public ClientViewsController(CsvDocumentWebViewerContext context, IClientViewRepository clientViewRepository)
        {
            _context = context;
            _clientViewRepository = clientViewRepository;
        }

        // GET: ClientViews
        public async Task<IActionResult> Index()
        {
            return View(await _clientViewRepository.GetAllAsync());
            //return View(await _context.ClientView.ToListAsync());
        }

        // GET: ClientViews/Details/5
        //public async Task<IActionResult> Details(int? id)
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clientView = _clientViewRepository.Get(id);
            //var clientView = await _context.ClientView
            //    .FirstOrDefaultAsync(m => m.ClientId == id);
            if (clientView == null)
            {
                return NotFound();
            }

            return View(clientView);
        }

        // GET: ClientViews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientViews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //public async Task<IActionResult> Create([Bind("ClientId,Name,Surname")] ClientView clientView)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ClientId,Name,Surname")] ClientView clientView)
        {
            if (ModelState.IsValid)
            {
                _clientViewRepository.Add(clientView);
                //_context.Add(clientView);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientView);
        }

        // GET: ClientViews/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientView =_clientViewRepository.Get(id);
            //var clientView = await _context.ClientView.FindAsync(id);
            if (clientView == null)
            {
                return NotFound();
            }
            return View(clientView);
        }

        // POST: ClientViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //public async Task<IActionResult> Edit(int id, [Bind("ClientId,Name,Surname")] ClientView clientView)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ClientId,Name,Surname")] ClientView clientView)
        {
            if (id != clientView.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _clientViewRepository.Update(clientView);
                    //_context.Update(clientView);
                    //await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientViewExists(clientView.ClientId))
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
            return View(clientView);
        }

        // GET: ClientViews/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clientView = _clientViewRepository.Get(id);
            //var clientView = await _context.ClientView
            //    .FirstOrDefaultAsync(m => m.ClientId == id);
            if (clientView == null)
            {
                return NotFound();
            }

            return View(clientView);
        }

        // POST: ClientViews/Delete/5
        //public async Task<IActionResult> DeleteConfirmed(int id)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _clientViewRepository.Delete(id);
            //var clientView = await _context.ClientView.FindAsync(id);
            //_context.ClientView.Remove(clientView);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientViewExists(int id)
        {
            return _clientViewRepository.Exists(id);
            //return _context.ClientView.Any(e => e.ClientId == id);
        }
    }
}
