using KooliProjekt.Models;

namespace KooliProjekt.Data.Repositories
{
    public interface ITeamMembersRepository
    {
        Task<PagedResult<TeamMembers>> List(int page, int pageSize);
        Task<TeamMembers> Get(int id);
        Task Save(TeamMembers member);
        Task Delete(int id);
    }
}
