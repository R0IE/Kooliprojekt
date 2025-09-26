using KooliProjekt.Models;

namespace KooliProjekt.Data.Repositories
{
    public interface IUserRepository
    {
        Task<PagedResult<User>> List(int page, int pageSize);
        Task<User> GetById(int id);
        Task Save(User user);
        Task Delete(int id);
    }
}
