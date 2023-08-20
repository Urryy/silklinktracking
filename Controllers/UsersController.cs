using cargosiklink.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace cargosiklink.Controllers
{
    public class UsersController : Controller
    {
        private readonly IAccountService _accountService;
        public UsersController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: UserController
        public async Task<IActionResult> Index()
        {
            var usersViewModel = (await _accountService.GetAll()).ToList();
            if(usersViewModel.Count == 0)
            {
                return View(usersViewModel);
            }
            else
            {
                return View(usersViewModel.OrderBy(item => item.UserCode).ToList());
            }
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if(id == Guid.Empty)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {
                var response = await _accountService.Delete(id);
                return RedirectToAction("Index", "Users");
            }
        }
    }
}
