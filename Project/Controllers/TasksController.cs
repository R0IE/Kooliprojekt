using KooliProjekt.Data;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    public class TasksController : Controller
    {
        private readonly ApplicationDbContext _dataContext;

        public TasksController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: TasksController
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var tasks = _dataContext.Tasks.GetPagedAsync(page, pageSize);

            return View(tasks);
        }

        // GET: TasksController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TasksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TasksController/Create
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

        // GET: TasksController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TasksController/Edit/5
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

        // GET: TasksController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TasksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
