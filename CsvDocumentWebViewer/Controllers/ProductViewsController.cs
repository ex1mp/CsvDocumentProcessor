using CsvDocumentWebViewer.Models;
using CsvDocumentWebViewer.Services.Models;
using CsvDocumentWebViewer.Services.ViewsRepository.PtoductViewRepo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Controllers
{
    public class ProductViewsController : Controller
    {
        private readonly CsvDocumentWebViewerContext _context;
        private readonly IProductViewRepository _productViewRepository;

        public ProductViewsController(CsvDocumentWebViewerContext context, IProductViewRepository productViewRepository)
        {
            _context = context;
            _productViewRepository = productViewRepository;
        }

        // GET: ProductViews
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(string ptoductName, int? pageNumber, string currentFilter)
        {
            if (ptoductName != null)
            {
                pageNumber = 1;
            }
            else
            {
                ptoductName = currentFilter;
            }
            ViewData["NameFilter"] = ptoductName;
            var products = await _productViewRepository.GetAllAsync();
            if (!String.IsNullOrEmpty(ptoductName))
            {
                products = products.Where(s => s.ProductName.ToUpper().Contains(ptoductName.ToUpper())).ToList();
            }
            int pageSize = 3;   
            return View(PaginatedList<ProductView>.Create(products, pageNumber ?? 1, pageSize));
            // return View(await _context.ProductView.ToListAsync());
        }

        
        //public IActionResult Index(string productName)
        //{ 
        //    return View(_productViewRepository.GetAll().Where(x=>x.ProductName == productName));
        //}
        // GET: ProductViews/Details/5
        //public async Task<IActionResult> Details(int? id)
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productView = _productViewRepository.Get(id);
            //var productView = await _context.ProductView
            //    .FirstOrDefaultAsync(m => m.ProductId == id);
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

        // public async Task<IActionResult> Create([Bind("ProductId,ProductName")] ProductView productView)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ProductId,ProductName")] ProductView productView)
        {
            if (ModelState.IsValid)
            {
                _productViewRepository.Add(productView);
                //_context.Add(productView);
                // await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productView);
        }

        // GET: ProductViews/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productView = _productViewRepository.Get(id);
            //var productView = await _context.ProductView.FindAsync(id);
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
        //public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName")] ProductView productView)
        public IActionResult Edit(int id, [Bind("ProductId,ProductName")] ProductView productView)
        {
            if (id != productView.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _productViewRepository.Update(productView);
                    // _context.Update(productView);
                    // await _context.SaveChangesAsync();
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
        // public async Task<IActionResult> Delete(int? id)
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productView = _productViewRepository.Get(id);
            //var productView = await _context.ProductView
            //    .FirstOrDefaultAsync(m => m.ProductId == id);
            if (productView == null)
            {
                return NotFound();
            }

            return View(productView);
        }

        // POST: ProductViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        public IActionResult DeleteConfirmed(int id)
        {
            //var productView = _productViewRepository.Get(id);
            _productViewRepository.Delete(id);
            // var productView = await _context.ProductView.FindAsync(id);
            // _context.ProductView.Remove(productView);
            // await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductViewExists(int id)
        {
            return _productViewRepository.Exists(id);
            // return _context.ProductView.Any(e => e.ProductId == id);
        }
    }
}
