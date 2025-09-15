using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace KooliProjekt.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        public DbSet<Project> Projects { get; set; }

        public DbSet<Tasks> Tasks { get; set; }

        public DbSet<team_members> TeamMembers { get; set; }

    public DbSet<User> AppUsers { get; set; }

        public DbSet<WorkLogs> WorkLogs { get; set; }
    }
}
