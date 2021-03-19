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
    public class ManagerViewsController : Controller
    {
        private readonly CsvDocumentWebViewerContext _context;

        public ManagerViewsController(CsvDocumentWebViewerContext context)
        {
            _context = context;
        }

        // GET: ManagerViews
        public async Task<IActionResult> Index()
        {
            return View(await _context.ManagerView.ToListAsync());
        }

        // GET: ManagerViews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerView = await _context.ManagerView
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (managerView == null)
            {
                return NotFound();
            }

            return View(managerView);
        }

        // GET: ManagerViews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManagerViews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ManagerId,Name,Surname,Post")] ManagerView managerView)
        {
            if (ModelState.IsValid)
            {
                _context.Add(managerView);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(managerView);
        }

        // GET: ManagerViews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerView = await _context.ManagerView.FindAsync(id);
            if (managerView == null)
            {
                return NotFound();
            }
            return View(managerView);
        }

        // POST: ManagerViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ManagerId,Name,Surname,Post")] ManagerView managerView)
        {
            if (id != managerView.ManagerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(managerView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ManagerViewExists(managerView.ManagerId))
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
            return View(managerView);
        }

        // GET: ManagerViews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerView = await _context.ManagerView
                .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (managerView == null)
            {
                return NotFound();
            }

            return View(managerView);
        }

        // POST: ManagerViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var managerView = await _context.ManagerView.FindAsync(id);
            _context.ManagerView.Remove(managerView);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerViewExists(int id)
        {
            return _context.ManagerView.Any(e => e.ManagerId == id);
        }
    }
}
