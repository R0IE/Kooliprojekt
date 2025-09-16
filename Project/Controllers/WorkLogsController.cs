using KooliProjekt.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    public class WorkLogsController : Controller
    {
        private readonly ApplicationDbContext _dataContext;

        public WorkLogsController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        // GET: WorkLogsController
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var workLogs = _dataContext.WorkLogs.GetPagedAsync(page, pageSize);

            return View(workLogs);
        }

        // GET: WorkLogsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WorkLogsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkLogsController/Create
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

        // GET: WorkLogsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WorkLogsController/Edit/5
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

        // GET: WorkLogsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WorkLogsController/Delete/5
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
