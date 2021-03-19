using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CsvDocumentWebViewer.Services.Models;

namespace CsvDocumentWebViewer.Controllers
{
    public class ClientViewsController : Controller
    {
        private readonly CsvDocumentWebViewerContext _context;

        public ClientViewsController(CsvDocumentWebViewerContext context)
        {
            _context = context;
        }

        // GET: ClientViews
        public async Task<IActionResult> Index()
        {
            return View(await _context.ClientView.ToListAsync());
        }

        // GET: ClientViews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientView = await _context.ClientView
                .FirstOrDefaultAsync(m => m.ClientId == id);
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,Name,Surname")] ClientView clientView)
        {
            if (ModelState.IsValid)
            {
                _context.Add(clientView);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clientView);
        }

        // GET: ClientViews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientView = await _context.ClientView.FindAsync(id);
            if (clientView == null)
            {
                return NotFound();
            }
            return View(clientView);
        }

        // POST: ClientViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClientId,Name,Surname")] ClientView clientView)
        {
            if (id != clientView.ClientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clientView);
                    await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientView = await _context.ClientView
                .FirstOrDefaultAsync(m => m.ClientId == id);
            if (clientView == null)
            {
                return NotFound();
            }

            return View(clientView);
        }

        // POST: ClientViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clientView = await _context.ClientView.FindAsync(id);
            _context.ClientView.Remove(clientView);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientViewExists(int id)
        {
            return _context.ClientView.Any(e => e.ClientId == id);
        }
    }
}
