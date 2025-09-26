using System.Threading.Tasks;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Data.Repositories
{
    public class TasksRepository : BaseRepository<Tasks>, ITasksRepository
    {
        public TasksRepository(ApplicationDbContext context) : base(context) { }

        public override async Task<PagedResult<Tasks>> List(int page, int pageSize)
        {
            return await base.List(page, pageSize);
        }

        public override async Task<Tasks> GetById(int id)
        {
            return await base.GetById(id);
        }

        public override async Task Save(Tasks entity)
        {
            await base.Save(entity);
        }

        public override async Task Delete(int id)
        {
            await base.Delete(id);
        }
    }
}
