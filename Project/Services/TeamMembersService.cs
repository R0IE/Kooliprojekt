using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class TeamMembersService : ITeamMembersService
    {
        private readonly ApplicationDbContext _context;

        public TeamMembersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<TeamMembers>> List(int page, int pageSize)
        {
            var teamMembers = await _context.TeamMembers.GetPagedAsync(page, pageSize);

            return teamMembers;
        }

        public async Task<TeamMembers> Get(int id)
        {
            var result = await _context.TeamMembers.FirstOrDefaultAsync(m => m.Id == id);

            return result;
        }

        public async Task Save(TeamMembers list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var member = await _context.TeamMembers.FindAsync(id);
            if (member != null)
            {
                _context.TeamMembers.Remove(member);
                await _context.SaveChangesAsync();
            }
        }
    }
}
