using KooliProjekt.Models;

namespace KooliProjekt.Data.Repositories
{
    public interface ITasksRepository
    {
        Task<PagedResult<Tasks>> List(int page, int pageSize);
        Task<Tasks> GetById(int id);
        Task Save(Tasks task);
        Task Delete(int id);
    }
}
