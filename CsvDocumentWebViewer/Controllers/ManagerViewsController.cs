using CsvDocumentWebViewer.Models;
using CsvDocumentWebViewer.Services.Models;
using CsvDocumentWebViewer.Services.ViewsRepository.ManagerViewRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Controllers
{
    [Authorize]
    public class ManagerViewsController : Controller
    {
        private readonly IManagerViewRepository _managerViewRepository;

        public ManagerViewsController(IManagerViewRepository managerViewRepository)
        {
            _managerViewRepository = managerViewRepository;
        }

        // GET: ManagerViews
        public async Task<IActionResult> Index(string managerSurname, string managerPost, int? pageNumber,
            string currentSurnameFilter, string currentPostFilter)
        {
            if (managerSurname != null || managerPost != null)
            {
                pageNumber = 1;
            }
            else
            {
                managerSurname = currentSurnameFilter;
                managerPost = currentPostFilter;
            }
            ViewData["SurnameFilter"] = managerSurname;
            ViewData["PostFilter"] = managerPost;
            var managers = await _managerViewRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(managerSurname))
            {
                managers = managers.Where(s => s.Surname.ToUpper().Contains(managerSurname.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(managerPost))
            {
                managers = managers.Where(s => s.Post.ToUpper().Contains(managerPost.ToUpper())).ToList();
            }
            int pageSize = 3;
            return View(PaginatedList<ManagerView>.Create(managers, pageNumber ?? 1, pageSize));
        }

        // GET: ManagerViews/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var managerView = _managerViewRepository.Get(id);

            if (managerView == null)
            {
                return NotFound();
            }

            return View(managerView);
        }

        // GET: ManagerViews/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManagerViews/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerView = _managerViewRepository.Get(id);

            if (managerView == null)
            {
                return NotFound();
            }
            return View(managerView);
        }

        // POST: ManagerViews/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
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
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var managerView = _managerViewRepository.Get(id);

            if (managerView == null)
            {
                return NotFound();
            }

            return View(managerView);
        }

        // POST: ManagerViews/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _managerViewRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ManagerViewExists(int id)
        {
            return _managerViewRepository.Exists(id);
        }
    }
}
