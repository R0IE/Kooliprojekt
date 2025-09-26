using KooliProjekt.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task<IActionResult> Index(int page = 1, int pageSize = 10)
        {
            var workLogs = await _dataContext.WorkLogs
                .OrderByDescending(w => w.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return View(workLogs);
        }

        // GET: WorkLogsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var workLog = await _dataContext.WorkLogs.FirstOrDefaultAsync(w => w.Id == id);
            if (workLog == null) return NotFound();
            return View(workLog);
        }

        // GET: WorkLogsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkLogsController/Create
    [HttpPost]
    public async Task<IActionResult> Create(WorkLogs workLog)
        {
            if (!ModelState.IsValid)
            {
                // Diagnostic: log model state errors so integration tests can surface validation problems
                foreach (var kvp in ModelState)
                {
                    var key = kvp.Key;
                    foreach (var err in kvp.Value.Errors)
                    {
                        System.Console.WriteLine($"ModelState error for '{key}': {err.ErrorMessage} | Exception: {err.Exception}");
                    }
                }

                return View(workLog);
            }

            _dataContext.WorkLogs.Add(workLog);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: WorkLogsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var workLog = await _dataContext.WorkLogs.FindAsync(id);
            if (workLog == null) return NotFound();
            return View(workLog);
        }

        // POST: WorkLogsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, WorkLogs workLog)
        {
            if (id != workLog.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View(workLog);
            }

            _dataContext.Entry(workLog).State = EntityState.Modified;
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: WorkLogsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var workLog = await _dataContext.WorkLogs.FirstOrDefaultAsync(w => w.Id == id);
            if (workLog == null) return NotFound();
            return View(workLog);
        }

        // POST: WorkLogsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var workLog = await _dataContext.WorkLogs.FindAsync(id);
            if (workLog == null) return NotFound();
            _dataContext.WorkLogs.Remove(workLog);
            await _dataContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
