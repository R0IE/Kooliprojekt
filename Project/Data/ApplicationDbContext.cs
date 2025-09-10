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
            public DbSet<project> project { get; set; }

            public DbSet<tasks> tasks { get; set; }

            public DbSet<team_members> team_Members { get; set; }
            
            public DbSet<user> user { get; set; }

            public DbSet<worklogs> worklogs { get; set; }

        }
    }

