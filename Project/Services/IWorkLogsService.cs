using KooliProjekt.Data;

namespace KooliProjekt.Services
{
        using KooliProjekt.Models;
    public interface IWorkLogsService
    {
            Task<PagedResult<WorkLogs>> List(int page, int pageSize);
            Task<WorkLogs> GetById(int id);
            Task Save(WorkLogs project);
            Task Delete(int id);

    }
}
