using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KooliProjekt.Services;
using KooliProjekt.Models;
using System.Linq;

namespace KooliProjekt.Controllers
{
    public class BeerController : Controller
    {
        private readonly BeerService _beerService;

        public BeerController(BeerService beerService)
        {
            _beerService = beerService;
        }

        public async Task<IActionResult> Index(int perPage = 20)
        {
            var breweries = await _beerService.GetBreweriesAsync(perPage);
            return View(breweries);
        }
    }
}
