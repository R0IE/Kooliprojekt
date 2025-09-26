using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
    [Microsoft.AspNetCore.Mvc.IgnoreAntiforgeryToken]
    public class ProjectController : Controller
    {
        private readonly IProjectService _projectService;
        private readonly IFileClient _fileClient;

        public ProjectController(IProjectService projectService, IFileClient fileClient)
        {
            _projectService = projectService;
            _fileClient = fileClient;
        }

        // GET: ProjectController
        public async Task<ActionResult> Index(int page = 1, int pageSize = 10)
        {
            var projects = await _projectService.List(page, pageSize);
            ViewBag.Files = _fileClient.List(FileStoreNames.Images);

            return View(projects);
        }

        // GET: ProjectController/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _projectService.GetById(id.Value);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: ProjectController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Project project, IFormFile[] files)
        {
            if (ModelState.IsValid)
            {
                await _projectService.Save(project);

                foreach (var file in files)
                {
                    _fileClient.Save(file.OpenReadStream(), file.FileName, FileStoreNames.Images);
                }

                return RedirectToAction(nameof(Index));
            }
            // Dump modelstate errors to console for test debugging
            Console.WriteLine("Create: ModelState.IsValid == false");
            foreach (var kv in ModelState)
            {
                foreach (var err in kv.Value.Errors)
                {
                    Console.WriteLine($"ModelState error on {kv.Key}: {err.ErrorMessage} {err.Exception}");
                }
            }

            return View(project);
        }

        // GET: ProjectController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var project = await _projectService.GetById(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Project project)
        {
            if (project == null)
            {
                Console.WriteLine("Edit: project was null after model binding");
                ModelState.AddModelError(string.Empty, "Invalid project data");
                return View(new Project());
            }

            if (id != project.Id)
            {
                Console.WriteLine($"Edit: Id mismatch route={id} project.Id={project.Id}");
                ModelState.AddModelError(string.Empty, "Id mismatch");
                return View(project);
            }

            if (ModelState.IsValid)
            {
                await _projectService.Save(project);
                return RedirectToAction(nameof(Index));
            }

            Console.WriteLine("Edit: ModelState.IsValid == false");
            foreach (var kv in ModelState)
            {
                foreach (var err in kv.Value.Errors)
                {
                    Console.WriteLine($"ModelState error on {kv.Key}: {err.ErrorMessage} {err.Exception}");
                }
            }

            return View(project);
        }

        // POST: ProjectController/DeleteFile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteFile(string fileName, string storeName)
        {
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                     _fileClient.Delete(fileName, storeName);
                    TempData["Message"] = "File successfully deleted.";
                }
                else
                {
                    TempData["Error"] = "Invalid file name.";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"An error occurred while attempting to delete the file: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: ProjectController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var project = await _projectService.GetById(id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: ProjectController/Delete/5
    [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _projectService.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                Console.WriteLine($"DeleteConfirmed: exception while deleting id={id}");
                return View();
            }
        }
    }
}
