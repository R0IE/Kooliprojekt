using FluentValidation;
using KooliProjekt.Data;
using KooliProjekt.Data.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();
            builder.Services.AddValidatorsFromAssemblyContaining<ProjectValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<TasksValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<TeamMembersValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<UserValidator>();
            builder.Services.AddValidatorsFromAssemblyContaining<WorkLogsValidator>();
            builder.Services.AddScoped<IFileClient, LocalFileClient>();
            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

#if (DEBUG)
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();

                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                IdentityUser user = new();
                user.UserName = "userd@mail.ee";
                user.EmailConfirmed = true;
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, "heromine");

                userManager.CreateAsync(user).Wait();

                if (dbContext.Project.Count() == 0)
                {
                    Project project = new Project();

                    project.Id = "1";
                    project.ProjectName = "Projekt #1";
                    project.Start = "2025-10-23";
                    project.Deadline = "2025-01-23";
                    project.Budget = "100000";
                    project.HourlyRate = "100";
                    project.Team = "Team #1";

                    dbContext.Project.Add(project);
                    dbContext.SaveChanges();
                }
            }
#endif

            app.Run();
        }
    }
}