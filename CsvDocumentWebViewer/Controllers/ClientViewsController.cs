using CsvDocumentWebViewer.Models;
using CsvDocumentWebViewer.Services.Models;
using CsvDocumentWebViewer.Services.ViewsRepository.ClientViewRepo;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CsvDocumentWebViewer.Controllers
{
    [Authorize]
    public class ClientViewsController : Controller
    {
        private readonly IClientViewRepository _clientViewRepository;

        public ClientViewsController(IClientViewRepository clientViewRepository)
        {
            _clientViewRepository = clientViewRepository;
        }

        // GET: ClientViews
        public async Task<IActionResult> Index(string clientSurname, string clientName, int? pageNumber,
            string currentSurnameFilter, string currentNameFilter)
        {
            if (clientSurname != null || clientName != null)
            {
                pageNumber = 1;
            }
            else
            {
                clientSurname = currentSurnameFilter;
                clientName = currentNameFilter;
            }
            ViewData["SurnameFilter"] = clientSurname;
            ViewData["NameFilter"] = clientName;
            var clients = await _clientViewRepository.GetAllAsync();
            if (!string.IsNullOrEmpty(clientSurname))
            {
                clients = clients.Where(s => s.Surname.ToUpper().Contains(clientSurname.ToUpper())).ToList();
            }
            if (!string.IsNullOrEmpty(clientName))
            {
                clients = clients.Where(s => s.Name.ToUpper().Contains(clientName.ToUpper())).ToList();
            }
            var pageSize = 3;
            return View(PaginatedList<ClientView>.Create(clients, pageNumber ?? 1, pageSize));
        }

        // GET: ClientViews/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clientView = _clientViewRepository.Get(id);

            if (clientView == null)
            {
                return NotFound();
            }

            return View(clientView);
        }

        // GET: ClientViews/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: ClientViews/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Create([Bind("ClientId,Name,Surname")] ClientView clientView)
        {
            if (!ModelState.IsValid)
            {
                return View(clientView);

            }
            _clientViewRepository.Add(clientView);
            return RedirectToAction(nameof(Index));
        }

        // GET: ClientViews/Edit/5
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clientView = _clientViewRepository.Get(id);

            if (clientView == null)
            {
                return NotFound();
            }
            return View(clientView);
        }

        // POST: ClientViews/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult Edit(int id, [Bind("ClientId,Name,Surname")] ClientView clientView)
        {
            if (id != clientView.ClientId)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(clientView);

            }
            try
            {
                _clientViewRepository.Update(clientView);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientViewExists(clientView.ClientId))
                {
                    return NotFound();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: ClientViews/Delete/5
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clientView = _clientViewRepository.Get(id);

            if (clientView == null)
            {
                return NotFound();
            }

            return View(clientView);
        }

        // POST: ClientViews/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            _clientViewRepository.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool ClientViewExists(int id)
        {
            return _clientViewRepository.Exists(id);
        }
    }
}
