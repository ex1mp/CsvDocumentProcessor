using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CsvDocumentWebViewer.Services.Models;
using CsvDocumentWebViewer.Services.ViewsRepository.ManagerViewRepo;

namespace CsvDocumentWebViewer.Controllers
{
    public class ManagerViewsController : Controller
    {
        private readonly CsvDocumentWebViewerContext _context;
        private readonly IManagerViewRepository _managerViewRepository;

        public ManagerViewsController(CsvDocumentWebViewerContext context, IManagerViewRepository managerViewRepository)
        {
            _context = context;
            _managerViewRepository = managerViewRepository;
        }

        // GET: ManagerViews
        public async Task<IActionResult> Index()
        {
            return View(await _managerViewRepository.GetAllAsync());
           // return View(await _context.ManagerView.ToListAsync());
        }

        // GET: ManagerViews/Details/5
        //public async Task<IActionResult> Details(int? id)
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var managerView = _managerViewRepository.Get(id);
            //var managerView = await _context.ManagerView
            //    .FirstOrDefaultAsync(m => m.ManagerId == id);
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

        //public async Task<IActionResult> Create([Bind("ManagerId,Name,Surname,Post")] ManagerView managerView)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ManagerId,Name,Surname,Post")] ManagerView managerView)
        {
            if (ModelState.IsValid)
            {
                _managerViewRepository.Add(managerView);
                //_context.Add(managerView);
                //await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(managerView);
        }

        // GET: ManagerViews/Edit/5
        //public async Task<IActionResult> Edit(int? id)
            public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerView = _managerViewRepository.Get(id);
            //var managerView = await _context.ManagerView.FindAsync(id);
            if (managerView == null)
            {
                return NotFound();
            }
            return View(managerView);
        }

        // POST: ManagerViews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //public async Task<IActionResult> Edit(int id, [Bind("ManagerId,Name,Surname,Post")] ManagerView managerView)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ManagerId,Name,Surname,Post")] ManagerView managerView)
        {
            if (id != managerView.ManagerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _managerViewRepository.Update(managerView);
                    //_context.Update(managerView);
                    //await _context.SaveChangesAsync();
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
        //public async Task<IActionResult> Delete(int? id)
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerView = _managerViewRepository.Get(id);
            //var managerView = await _context.ManagerView
            //    .FirstOrDefaultAsync(m => m.ManagerId == id);
            if (managerView == null)
            {
                return NotFound();
            }

            return View(managerView);
        }

        // POST: ManagerViews/Delete/5
        //public async Task<IActionResult> DeleteConfirmed(int id)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _managerViewRepository.Delete(id);
            //var managerView = await _context.ManagerView.FindAsync(id);
            //_context.ManagerView.Remove(managerView);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerViewExists(int id)
        {
            return _managerViewRepository.Exists(id);
            //return _context.ManagerView.Any(e => e.ManagerId == id);
        }
    }
}
