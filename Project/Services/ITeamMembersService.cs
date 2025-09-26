using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Models;

namespace KooliProjekt.Services
{
    public interface ITeamMembersService
    {
    Task<PagedResult<TeamMembers>> List(int page, int pageSize);
        Task<TeamMembers> Get(int id);
        Task Save(TeamMembers member);
        Task Delete(int id);
    }
}
