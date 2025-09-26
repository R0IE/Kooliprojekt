using KooliProjekt.Data;
using KooliProjekt.Models;

namespace KooliProjekt.Services
{
    public interface ITasksService
    {
        Task<PagedResult<Tasks>> List(int page = 1, int pageSize = 10);
        Task<Tasks> GetById(int id);
        Task Save(Tasks task);
        Task Delete(int id);
    }
}
