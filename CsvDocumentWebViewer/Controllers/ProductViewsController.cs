using CsvDocumentWebViewer.Models;
using CsvDocumentWebViewer.Services.Models;
using CsvDocumentWebViewer.Services.ViewsRepository.PtoductViewRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Controllers
{
    [Authorize]
    public class ProductViewsController : Controller
    {
        private readonly IProductViewRepository _productViewRepository;

        public ProductViewsController(IProductViewRepository productViewRepository)
        {
            _productViewRepository = productViewRepository;
        }

        // GET: ProductViews
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
            if (!string.IsNullOrEmpty(ptoductName))
            {
                products = products.Where(s => s.ProductName.ToUpper().Contains(ptoductName.ToUpper())).ToList();
            }
            int pageSize = 3;
            return View(PaginatedList<ProductView>.Create(products, pageNumber ?? 1, pageSize));
        }


        // GET: ProductViews/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productView = _productViewRepository.Get(id);
            if (productView == null)
            {
                return NotFound();
            }

            return View(productView);
        }

        // GET: ProductViews/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductViews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create([Bind("ProductId,ProductName")] ProductView productView)
        {
            if (ModelState.IsValid)
            {
                _productViewRepository.Add(productView);
                return RedirectToAction(nameof(Index));
            }
            return View(productView);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productView = _productViewRepository.Get(id);

            if (productView == null)
            {
                return NotFound();
            }
            return View(productView);
        }

        // POST: ProductViews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productView = _productViewRepository.Get(id);
            if (productView == null)
            {
                return NotFound();
            }

            return View(productView);
        }

        // POST: ProductViews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _productViewRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ProductViewExists(int id)
        {
            return _productViewRepository.Exists(id);
        }
    }
}
