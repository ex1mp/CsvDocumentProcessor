﻿using CsvDocumentWebViewer.Models;
using CsvDocumentWebViewer.Services.ModelsView;
using CsvDocumentWebViewer.Services.ViewsRepository.ClientViewRepo;
using CsvDocumentWebViewer.Services.ViewsRepository.ManagerViewRepo;
using CsvDocumentWebViewer.Services.ViewsRepository.PtoductViewRepo;
using CsvDocumentWebViewer.Services.ViewsRepository.SalesViewRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Controllers
{
    [Authorize]
    public class SalesViewsController : Controller
    {
        private readonly ISalesViewRepository _salesViewRepository;
        private readonly IClientViewRepository _clientViewRepository;
        private readonly IProductViewRepository _productViewRepository;
        private readonly IManagerViewRepository _managerViewRepository;

        public SalesViewsController(ISalesViewRepository salesViewRepository,
             IClientViewRepository clientViewRepository, IManagerViewRepository managerViewRepository,
             IProductViewRepository productViewRepository)
        {
            _salesViewRepository = salesViewRepository;
            _clientViewRepository = clientViewRepository;
            _productViewRepository = productViewRepository;
            _managerViewRepository = managerViewRepository;
        }

        // GET: SalesViews
        public async Task<IActionResult> Index(DateTime startDate, DateTime endDate, decimal minSum, decimal maxSum,
            int? pageNumber, DateTime currentStartDateFilter, DateTime currentEndDateFilter,
            decimal currentMinSumFilter, decimal currentMaxSumFilter)
        {
            if (startDate != default || endDate != default
                || minSum != default || maxSum != default)
            {
                pageNumber = 1;
            }
            else
            {
                startDate = currentStartDateFilter;
                endDate = currentEndDateFilter;
                minSum = currentMinSumFilter;
                maxSum = currentMaxSumFilter;
            }
            ViewData["StartDate"] = startDate;
            ViewData["EndDate"] = endDate;
            ViewData["MinSum"] = minSum;
            ViewData["MaxSum"] = maxSum;
            var salesViews = await _salesViewRepository.GetAllAsync();
            if (startDate != default)
            {
                salesViews = salesViews.Where(x => x.SaleDate > startDate).ToList();
            }
            if (endDate != default)
            {
                salesViews = salesViews.Where(x => x.SaleDate < endDate).ToList();
            }
            if (minSum != default)
            {
                salesViews = salesViews.Where(x => x.SaleCost > minSum).ToList();
            }
            if (maxSum != default)
            {
                salesViews = salesViews.Where(x => x.SaleCost < maxSum).ToList();
            }
            var pageSize = 5;
            return View(PaginatedList<SalesView>.Create(salesViews, pageNumber ?? 1, pageSize));
        }


        // GET: SalesViews/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesView = _salesViewRepository.Get(id);
            if (salesView == null)
            {
                return NotFound();
            }

            return View(salesView);
        }

        // GET: SalesViews/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(_clientViewRepository.GetAll(), "ClientId", "ClientId");
            ViewData["ManagerId"] = new SelectList(_managerViewRepository.GetAll(), "ManagerId", "ManagerId");
            ViewData["ProductId"] = new SelectList(_productViewRepository.GetAll(), "ProductId", "ProductId");
            return View();
        }

        // POST: SalesViews/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create([Bind("SalesId,ManagerId,ClientId,ProductId,SaleDate,SaleCost")] SalesView salesView)
        {
            if (ModelState.IsValid)
            {
                _salesViewRepository.Add(salesView);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_clientViewRepository.GetAll(), "ClientId", "ClientId");
            ViewData["ManagerId"] = new SelectList(_managerViewRepository.GetAll(), "ManagerId", "ManagerId");
            ViewData["ProductId"] = new SelectList(_productViewRepository.GetAll(), "ProductId", "ProductId");
            return View(salesView);
        }

        // GET: SalesViews/Edit/5
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesView = _salesViewRepository.Get(id);

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SalesViewExists(salesView.SalesId))
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(_clientViewRepository.GetAll(), "ClientId", "ClientId");
            ViewData["ManagerId"] = new SelectList(_managerViewRepository.GetAll(), "ManagerId", "ManagerId");
            ViewData["ProductId"] = new SelectList(_productViewRepository.GetAll(), "ProductId", "ProductId");
            return View(salesView);
        }

        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var salesView = _salesViewRepository.Get(id);

            if (salesView == null)
            {
                return NotFound();
            }

            return View(salesView);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _salesViewRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool SalesViewExists(int id)
        {
            return _salesViewRepository.Exists(id);
        }
        public string GetAnalytics()
        {
            var s = _salesViewRepository.GetAllAsync();
            var d = s.Result.Where(x=>x.SaleDate.Month==DateTime.Now.Month && x.SaleDate.Year== DateTime.Now.Year)
                .GroupBy(x => x.SaleDate.Date)
                .Select(f => new
                {
                    date = f.Key.Date,
                    profit = f.Sum(w => w.SaleCost)
                }).ToList();

            d.Sort((x, y) => DateTime.Compare(x.date, y.date));
            var e = d.Select(f => new
            {
                date = f.date.ToShortDateString(),
                profit = f.profit
            });
            return JsonConvert.SerializeObject(e);
        }
        public IActionResult Analytics()
        {
            return View();
        }
    }
}
