using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CsvDocumentWebViewer.Services.Models;
using Microsoft.AspNetCore.Authorization;

namespace CsvDocumentWebViewer.Controllers
{
    [Authorize]
    public class SalesViewsController : Controller
    {
        private readonly CsvDocumentWebViewerContext _context;

        public SalesViewsController(CsvDocumentWebViewerContext context)
        {
            _context = context;
        }

        // GET: SalesViews
        public async Task<IActionResult> Index()
        {
            var csvDocumentWebViewerContext = _context.SalesView.Include(s => s.Client).Include(s => s.Manager).Include(s => s.Product);
            return View(await csvDocumentWebViewerContext.ToListAsync());
        }


        // GET: SalesViews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesView = await _context.SalesView
                .Include(s => s.Client)
                .Include(s => s.Manager)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.SalesId == id);
            if (salesView == null)
            {
                return NotFound();
            }

            return View(salesView);
        }

        // GET: SalesViews/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_context.Set<ClientView>(), "ClientId", "ClientId");
            ViewData["ManagerId"] = new SelectList(_context.Set<ManagerView>(), "ManagerId", "ManagerId");
            ViewData["ProductId"] = new SelectList(_context.Set<ProductView>(), "ProductId", "ProductId");
            return View();
        }

        // POST: SalesViews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SalesId,ManagerId,ClientId,ProductId,SaleDate,SaleCost")] SalesView salesView)
        {
            if (ModelState.IsValid)
            {
                _context.Add(salesView);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("Error");
        }

        // GET: SalesViews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesView = await _context.SalesView.FindAsync(id);
            if (salesView == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_context.ClientView.AsEnumerable(), "ClientId", "ClientId", salesView.ClientId);
            //ViewData["ManagerId"] = new SelectList(_context.Set<ManagerView>(), "ManagerId", "ManagerId", salesView.ManagerId);
           // ViewData["ProductId"] = new SelectList(_context.Set<ProductView>(), "ProductId", "ProductId", salesView.ProductId);
            return View(salesView);
        }

        // POST: SalesViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SalesId,ManagerId,ClientId,ProductId,SaleDate,SaleCost")] SalesView salesView)
        {
            if (id != salesView.SalesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(salesView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesViewExists(salesView.SalesId))
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
            ViewData["ClientId"] = new SelectList(_context.Set<ClientView>(), "ClientId", "ClientId", salesView.ClientId);
            ViewData["ManagerId"] = new SelectList(_context.Set<ManagerView>(), "ManagerId", "ManagerId", salesView.ManagerId);
            ViewData["ProductId"] = new SelectList(_context.Set<ProductView>(), "ProductId", "ProductId", salesView.ProductId);
            return View(salesView);
        }

        // GET: SalesViews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesView = await _context.SalesView
                .Include(s => s.Client)
                .Include(s => s.Manager)
                .Include(s => s.Product)
                .FirstOrDefaultAsync(m => m.SalesId == id);
            if (salesView == null)
            {
                return NotFound();
            }

            return View(salesView);
        }

        // POST: SalesViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var salesView = await _context.SalesView.FindAsync(id);
            _context.SalesView.Remove(salesView);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesViewExists(int id)
        {
            return _context.SalesView.Any(e => e.SalesId == id);
        }
    }
}
