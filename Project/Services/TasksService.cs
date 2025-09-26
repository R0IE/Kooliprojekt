using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Services
{
    public class TasksService : ITasksService
    {
        private readonly ApplicationDbContext _context;

        public TasksService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Tasks>> List(int page = 1, int pageSize = 10)
        {
            var result = await _context.Tasks.OrderBy(t => t.Id).GetPagedAsync(page, pageSize);
            return result;
        }

        public async Task<Tasks> GetById(int id)
        {
            var result = await _context.Tasks.FirstOrDefaultAsync(m => m.Id == id);

            return result;
        }

        public async Task Save(Tasks task)
        {
    
            var maxSqlTime = TimeSpan.FromDays(1) - TimeSpan.FromTicks(1);
            if (task.ExpectedTime >= TimeSpan.FromDays(1))
            {
                task.ExpectedTime = maxSqlTime;
            }

            if (task.ProjectId == 0)
            {
                var firstProjectId = await _context.Project.Select(p => p.Id).OrderBy(id => id).FirstOrDefaultAsync();
                if (firstProjectId == 0)
                {
                    throw new InvalidOperationException("Cannot save Task: no Project exists in the database. Create a Project first.");
                }
                task.ProjectId = firstProjectId;
            }
            else
            {
                var exists = await _context.Project.AnyAsync(p => p.Id == task.ProjectId);
                if (!exists)
                {
                    throw new InvalidOperationException($"Cannot save Task: Project with Id {task.ProjectId} does not exist.");
                }
            }

            if (task.Id == 0)
            {
                _context.Add(task);
            }
            else
            {
                _context.Update(task);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                await _context.SaveChangesAsync();
            }
        }
    }
}
