using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CsvDocumentWebViewer.Services.Models;
using Microsoft.AspNetCore.Authorization;
using CsvDocumentWebViewer.Services.ViewsRepository.SalesViewRepo;
using CsvDocumentWebViewer.Services.ViewsRepository.ClientViewRepo;
using CsvDocumentWebViewer.Services.ViewsRepository.ManagerViewRepo;
using CsvDocumentWebViewer.Services.ViewsRepository.PtoductViewRepo;
using CsvDocumentWebViewer.Models;

namespace CsvDocumentWebViewer.Controllers
{
    [Authorize]
    public class SalesViewsController : Controller
    {
        private readonly CsvDocumentWebViewerContext _context;
        private readonly ISalesViewRepository _salesViewRepository;
        private readonly IClientViewRepository _clientViewRepository;
        private readonly IProductViewRepository _productViewRepository;
        private readonly IManagerViewRepository _managerViewRepository;

       public SalesViewsController(CsvDocumentWebViewerContext context, ISalesViewRepository salesViewRepository,
            IClientViewRepository clientViewRepository, IManagerViewRepository managerViewRepository,
            IProductViewRepository productViewRepository)
        {
            _context = context;
            _salesViewRepository = salesViewRepository;
            _clientViewRepository = clientViewRepository;
            _productViewRepository = productViewRepository;
            _managerViewRepository = managerViewRepository;
        }

        // GET: SalesViews
        public async Task<IActionResult> Index(DateTime startDate, DateTime endDate,decimal minSum, decimal maxSum,
            int? pageNumber, DateTime currentStartDateFilter, DateTime currentEndDateFilter,
            decimal currenMinSumFilter, decimal currentMaxSumFilter)
        {
            if (startDate != default || endDate != default
                || minSum!=default || maxSum != default)
            {
                pageNumber = 1;
            }
            else
            {
                startDate = currentStartDateFilter;
                endDate = currentEndDateFilter;
                minSum = currenMinSumFilter;
                maxSum = currentMaxSumFilter;
            }
            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            ViewData["MinSum"] = minSum;
            ViewData["MaxSum"] = maxSum;
            var salesViews = await _salesViewRepository.GetAllAsync();
            if (startDate!= default(DateTime))
            {
                salesViews = salesViews.Where(x => x.SaleDate > startDate).ToList();
            }
            if (endDate != default(DateTime))
            {
                salesViews = salesViews.Where(x => x.SaleDate <endDate).ToList();
            }
            if (minSum != default(decimal))
            {
                salesViews = salesViews.Where(x => x.SaleCost> minSum).ToList();
            }
            if (maxSum != default(decimal))
            {
                salesViews = salesViews.Where(x => x.SaleCost < maxSum).ToList();
            }
            // var csvDocumentWebViewerContext = await _salesViewRepository.GetAllAsync();

            // var csvDocumentWebViewerContext = _context.SalesView.Include(s => s.Client).Include(s => s.Manager).Include(s => s.Product);
            // return View(salesViews);
            int pageSize = 5;
            return View(PaginatedList<SalesView>.Create(salesViews, pageNumber ?? 1, pageSize));
        }


        // GET: SalesViews/Details/5
        //public async Task<IActionResult> Details(int? id)
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesView = _salesViewRepository.Get(id);
            //var salesView = await _context.SalesView
            //    .Include(s => s.Client)
            //    .Include(s => s.Manager)
            //    .Include(s => s.Product)
            //    .FirstOrDefaultAsync(m => m.SalesId == id);
            if (salesView == null)
            {
                return NotFound();
            }

            return View(salesView);
        }

        // GET: SalesViews/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_clientViewRepository.GetAll(), "ClientId", "ClientId");
            ViewData["ManagerId"] = new SelectList(_managerViewRepository.GetAll(), "ManagerId", "ManagerId");
            ViewData["ProductId"] = new SelectList(_productViewRepository.GetAll(), "ProductId", "ProductId");
            return View();
        }

        // POST: SalesViews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //public async Task<IActionResult> Create([Bind("SalesId,ManagerId,ClientId,ProductId,SaleDate,SaleCost")] SalesView salesView)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("SalesId,ManagerId,ClientId,ProductId,SaleDate,SaleCost")] SalesView salesView)
        {
            if (ModelState.IsValid)
            {
                _salesViewRepository.Add(salesView);
                //_context.Add(salesView);
               // await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_clientViewRepository.GetAll(), "ClientId", "ClientId");
            ViewData["ManagerId"] = new SelectList(_managerViewRepository.GetAll(), "ManagerId", "ManagerId");
            ViewData["ProductId"] = new SelectList(_productViewRepository.GetAll(), "ProductId", "ProductId");
            return View(salesView);
        }

        // GET: SalesViews/Edit/5
        // public async Task<IActionResult> Edit(int? id)
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesView = _salesViewRepository.Get(id);
            //var salesView = await _context.SalesView.FindAsync(id);
            if (salesView == null)
            {
                return NotFound();
            }
            ViewData["ClientId"] = new SelectList(_clientViewRepository.GetAll(), "ClientId", "ClientId");
            ViewData["ManagerId"] = new SelectList(_managerViewRepository.GetAll(), "ManagerId", "ManagerId");
            ViewData["ProductId"] = new SelectList(_productViewRepository.GetAll(), "ProductId", "ProductId");
            return View(salesView);
        }

        // POST: SalesViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //public async Task<IActionResult> Edit(int id, [Bind("SalesId,ManagerId,ClientId,ProductId,SaleDate,SaleCost")] SalesView salesView)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("SalesId,ManagerId,ClientId,ProductId,SaleDate,SaleCost")] SalesView salesView)
        {
            if (id != salesView.SalesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _salesViewRepository.Update(salesView);
                   // _context.Update(salesView);
                   // await _context.SaveChangesAsync();
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
            ViewData["ClientId"] = new SelectList(_clientViewRepository.GetAll(), "ClientId", "ClientId");
            ViewData["ManagerId"] = new SelectList(_managerViewRepository.GetAll(), "ManagerId", "ManagerId");
            ViewData["ProductId"] = new SelectList(_productViewRepository.GetAll(), "ProductId", "ProductId");
            return View(salesView);
        }

        // GET: SalesViews/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesView = _salesViewRepository.Get(id);
            //var salesView = await _context.SalesView
            //    .Include(s => s.Client)
            //    .Include(s => s.Manager)
            //    .Include(s => s.Product)
            //    .FirstOrDefaultAsync(m => m.SalesId == id);
            if (salesView == null)
            {
                return NotFound();
            }

            return View(salesView);
        }

        // POST: SalesViews/Delete/5
        //public async Task<IActionResult> DeleteConfirmed(int id)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _salesViewRepository.Delete(id);
            //var salesView = await _context.SalesView.FindAsync(id);
            //_context.SalesView.Remove(salesView);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SalesViewExists(int id)
        {
            return _salesViewRepository.Exists(id);
           // return _context.SalesView.Any(e => e.SalesId == id);
        }
    }
}
