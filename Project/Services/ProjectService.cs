using KooliProjekt.Data;
using KooliProjekt.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KooliProjekt.Services
{
    public class ProjectService : IProjectService
    {
        private readonly ApplicationDbContext _context;

        public ProjectService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<Project>> List(int page = 1, int pageSize = 10)
        {
            var result = await _context.Project.OrderBy(p => p.Id).GetPagedAsync(page, pageSize);
            return result;
        }

        public async Task<Project> GetById(int id)
        {
            return await _context.Project.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save(Project project)
        {
            if (project.Id == 0)
            {
                _context.Add(project);
            }
            else
            {
                _context.Update(project);
            }

            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var project = await _context.Project.FindAsync(id);
            if (project != null)
            {
                _context.Project.Remove(project);
            }

            await _context.SaveChangesAsync();
        }
    }
}
