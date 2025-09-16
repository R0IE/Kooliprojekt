using KooliProjekt.Data;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.Controllers
{
    public class Team_MembersController : Controller
    {
        private readonly ApplicationDbContext _dataContext;

        public Team_MembersController(ApplicationDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult Index(int page = 1, int pageSize = 10)
        {
            var team_members = _dataContext.Team_Members.GetPagedAsync(page, pageSize);

            return View(team_members);
        }
    }
}
