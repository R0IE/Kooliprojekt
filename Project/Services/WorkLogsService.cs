using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class WorkLogsService : IWorkLogsService
    {
        private readonly ApplicationDbContext _context;

        public WorkLogsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<WorkLogs>> List(int page, int pageSize)
        {
            var result = await _context.WorkLogs.GetPagedAsync(page, pageSize);

            return result;
        }

        public async Task<WorkLogs> GetById(int id)
        {
            var result = await _context.WorkLogs.FirstOrDefaultAsync(m => m.Id == id);

            return result;
        }

        public async Task Save(WorkLogs list)
        {
            if (list.Id == 0)
            {
                _context.Add(list);
            }
            else
            {
                _context.Update(list);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var worklogs = await _context.WorkLogs.FindAsync(id);
            if (worklogs != null)
            {
                _context.WorkLogs.Remove(worklogs);
            }

            await _context.SaveChangesAsync();
        }
    }
}