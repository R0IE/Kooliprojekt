using System.Threading.Tasks;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class WorkLogsRepository : BaseRepository<WorkLogs>, IWorkLogsRepository
    {
        public WorkLogsRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<PagedResult<WorkLogs>> List(int page, int pageSize)
        {
            return await base.List(page, pageSize);
        }

        public override async Task<WorkLogs> GetById(int id)
        {
            return await base.GetById(id);
        }

        public override async Task Save(WorkLogs entity)
        {
            await base.Save(entity);
        }

        public override async Task Delete(int id)
        {
            await base.Delete(id);
        }
    }
}
