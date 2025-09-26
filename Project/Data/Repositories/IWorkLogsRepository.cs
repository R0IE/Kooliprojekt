using KooliProjekt.Models;

namespace KooliProjekt.Data.Repositories
{
    public interface IWorkLogsRepository
    {
        Task<PagedResult<WorkLogs>> List(int page, int pageSize);
        Task<WorkLogs> GetById(int id);
        Task Save(WorkLogs workLog);
        Task Delete(int id);
    }
}
