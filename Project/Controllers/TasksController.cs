using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    public class TasksController : Controller
    {
        private readonly ITasksService _tasksService;

        public TasksController(ITasksService tasksService)
        {
            _tasksService = tasksService;
        }

        // GET: TasksController
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var tasks = await _tasksService.List(page, pageSize);

            return View(tasks);
        }

        // GET: TasksController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _tasksService.GetById(id.Value);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // GET: TasksController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TasksController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _tasksService.Save(tasks);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
                catch (Exception ex)
                {
                    // Log exception (left as console for now) and show generic error
                    Console.WriteLine(ex);
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the task.");
                }
            }
            return View(tasks);
            //return BadRequest(ModelState);
        }

        // GET: TasksController/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Tasks = await _tasksService.GetById(id.Value);
            if (Tasks == null)
            {
                return NotFound();
            }
            return View(Tasks);
        }

        // POST: TasksController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Tasks tasks)
        {
            if (id != tasks.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(tasks);
            }

            await _tasksService.Save(tasks);

            return RedirectToAction(nameof(Index));
        }

        // GET: TasksController/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tasks = await _tasksService.GetById(id.Value);
            if (tasks == null)
            {
                return NotFound();
            }

            return View(tasks);
        }

        // POST: TasksController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _tasksService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
