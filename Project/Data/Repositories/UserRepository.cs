using System.Threading.Tasks;
using KooliProjekt.Models;

namespace KooliProjekt.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BaseRepository<User> _baseRepository;

        public UserRepository(ApplicationDbContext context)
        {
            _baseRepository = new BaseRepository<User>(context);
        }

        public async Task<PagedResult<User>> List(int page, int pageSize)
        {
            return await _baseRepository.List(page, pageSize);
        }

        public async Task<User> GetById(int id)
        {
            return await _baseRepository.GetById(id);
        }

        public async Task Save(User user)
        {
            await _baseRepository.Save(user);
        }

        public async Task Delete(int id)
        {
            await _baseRepository.Delete(id);
        }
    }
}
