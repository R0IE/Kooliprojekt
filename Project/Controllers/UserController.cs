using KooliProjekt.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using KooliProjekt.Services;

namespace KooliProjekt.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: UserController
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var users = await _userService.List(page, pageSize);
            return View(users);
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // GET: UserController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,PhoneNumber,Department")] KooliProjekt.Data.User user)
        {
            if (!ModelState.IsValid) return View(user);
            await _userService.Save(user);
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,PhoneNumber,Department")] KooliProjekt.Data.User user)
        {
            if (id != user.Id) return BadRequest();
            if (!ModelState.IsValid) return View(user);
            await _userService.Save(user);
            return RedirectToAction(nameof(Index));
        }

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetById(id);
            if (user == null) return NotFound();
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
