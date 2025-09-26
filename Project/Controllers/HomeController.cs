using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KooliProjekt.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ILogger<HomeController> _logger;
        //private readonly ApplicationDbContext _dataContext;

        public HomeController(
            ILogger<HomeController> logger
            // ApplicationDbContext dataContext,
            // IFileClient fileClient
        ) {
            _logger = logger;
            //_dataContext = dataContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile[] files)
        {


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}