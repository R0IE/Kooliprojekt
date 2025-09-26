using KooliProjekt.Models;

namespace KooliProjekt.Data.Repositories
{
    public interface IProjectRepository
    {
        Task<PagedResult<Project>> List(int page, int pageSize);
        Task<Project> GetById(int id);
        Task Save(Project project);
        Task Delete(int id);
    }
}
