using KooliProjekt.Data;
using KooliProjekt.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public interface IProjectService
    {
        Task<KooliProjekt.Models.PagedResult<Project>> List(int page = 1, int pageSize = 10);
        Task<Project> GetById(int id);
        Task Save(Project project);
        Task Delete(int id);
    }
}
