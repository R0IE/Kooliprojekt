using System.Threading.Tasks;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class TeamMembersRepository : BaseRepository<TeamMembers>, ITeamMembersRepository
    {
        public TeamMembersRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PagedResult<TeamMembers>> List(int page, int pageSize)
        {
            return await base.List(page, pageSize);
        }

        public async Task<TeamMembers> GetById(int id)
        {
            return await base.GetById(id);
        }

        public async Task Save(TeamMembers teamMember)
        {
            await base.Save(teamMember);
        }

        public async Task Delete(int id)
        {
            await base.Delete(id);
        }
        public async Task<TeamMembers> Get(int id)
        {
            return await base.GetById(id);
        }
    }
}
