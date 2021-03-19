﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CsvDocumentWebViewer.Services.Models;

namespace CsvDocumentWebViewer.Controllers
{
    public class ProductViewsController : Controller
    {
        private readonly CsvDocumentWebViewerContext _context;

        public ProductViewsController(CsvDocumentWebViewerContext context)
        {
            _context = context;
        }

        // GET: ProductViews
        public async Task<IActionResult> Index()
        {
            return View(await _context.ProductView.ToListAsync());
        }

        // GET: ProductViews/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productView = await _context.ProductView
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productView == null)
            {
                return NotFound();
            }

            return View(productView);
        }

        // GET: ProductViews/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductViews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName")] ProductView productView)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productView);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productView);
        }

        // GET: ProductViews/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productView = await _context.ProductView.FindAsync(id);
            if (productView == null)
            {
                return NotFound();
            }
            return View(productView);
        }

        // POST: ProductViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName")] ProductView productView)
        {
            if (id != productView.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productView);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductViewExists(productView.ProductId))
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
            return View(productView);
        }

        // GET: ProductViews/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productView = await _context.ProductView
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productView == null)
            {
                return NotFound();
            }

            return View(productView);
        }

        // POST: ProductViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productView = await _context.ProductView.FindAsync(id);
            _context.ProductView.Remove(productView);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductViewExists(int id)
        {
            return _context.ProductView.Any(e => e.ProductId == id);
        }
    }
}
