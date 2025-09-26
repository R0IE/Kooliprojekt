using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KooliProjekt.Controllers
{
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
            if (id != project.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _projectService.Save(project);
                return RedirectToAction(nameof(Index));
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
                return View();
            }
        }
    }
}
