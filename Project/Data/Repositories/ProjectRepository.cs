using KooliProjekt.Models;

namespace KooliProjekt.Data.Repositories
{
    public class ProjectRepository : BaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PagedResult<Project>> List(int page, int pageSize)
        {
            return await base.List(page, pageSize);
        }

        public async Task<Project> GetById(int id)
        {
            return await base.GetById(id);
        }

        public async Task Save(Project project)
        {
            await base.Save(project);
        }

        public async Task Delete(int id)
        {
            await base.Delete(id);
        }
        public async Task<Project> Get(int id)
        {
            return await base.GetById(id);
        }
    }
}
