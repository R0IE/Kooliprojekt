using System.Threading.Tasks;
using KooliProjekt.Data;
using KooliProjekt.Models;

namespace KooliProjekt.Services
{
    public interface IUserService
    {
        Task<PagedResult<User>> List(int page, int pageSize);
        Task<User> GetById(int id);
        Task Save(User user);
        Task Delete(int id);
    }
}
