using KooliProjekt.Data;
using KooliProjekt.Data.Repositories;

namespace KooliProjekt.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public IProjectRepository ProjectRepository { get; }
        public ITasksRepository TasksRepository { get; }
        public ITeamMembersRepository TeamMembersRepository { get; }
        public IUserRepository UserRepository { get; }
        public IWorkLogsRepository WorkLogsRepository { get; }


        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context, IProjectRepository projectRepository)
        {
            _context = context;

            ProjectRepository= projectRepository;
        }

        public async Task BeginTransaction()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
            await _context.Database.CommitTransactionAsync();
        }

        public async Task Rollback()
        {
            await _context.Database.RollbackTransactionAsync();
        }
    }
}
