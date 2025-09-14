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
            public DbSet<Project> project { get; set; }

            public DbSet<Tasks> tasks { get; set; }

            public DbSet<team_members> team_Members { get; set; }
            
            public DbSet<User> user { get; set; }

            public DbSet<WorkLogs> worklogs { get; set; }

        }
    }
