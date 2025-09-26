using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<User>> List(int page, int pageSize)
        {
            var result = await _context.User.GetPagedAsync(page, pageSize);

            return result;
        }

        public async Task<User> GetById(int id)
        {
            var result = await _context.User.FirstOrDefaultAsync(m => m.Id == id);

            return result;
        }

        public async Task Save(User user)
        {
            if (user.Id == 0)
            {
                // Ensure CreatedAt is set for new users to satisfy non-nullable DB column
                if (user.CreatedAt == default)
                {
                    user.CreatedAt = DateTime.UtcNow;
                }
                _context.Add(user);
            }
            else
            {
                _context.Update(user);
            }

            await _context.SaveChangesAsync();
        }


        public async Task Delete(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
            }

            await _context.SaveChangesAsync();
        }
    }
}