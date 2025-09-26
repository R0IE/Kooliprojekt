using FluentValidation;
using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.Models;
using KooliProjekt.Data.Repositories;
using KooliProjekt.Data.Validation;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace KooliProjekt
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
            builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
            builder.Services.AddScoped<IProjectService, ProjectService>();
            builder.Services.AddScoped<ITasksRepository, TasksRepository>();
            builder.Services.AddScoped<ITasksService, TasksService>();
            builder.Services.AddScoped<ITeamMembersRepository, TeamMembersRepository>();
            builder.Services.AddScoped<ITeamMembersService, TeamMembersService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IWorkLogsRepository, WorkLogsRepository>();
            builder.Services.AddScoped<IWorkLogsService, WorkLogsService>();

            builder.Services.AddHttpClient<IBeerClient, BeerClient>();
            builder.Services.AddScoped<BeerService>();

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
                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    await dbContext.Database.MigrateAsync();

                    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                    var existing = await userManager.FindByNameAsync("user");
                    if (existing == null)
                    {
            
                        var basePart = Guid.NewGuid().ToString("N").Substring(0, 8);
                        var devPassword = basePart + "A1!";
                        IdentityUser devUser = new() { UserName = "user", Email = "user@example.com", EmailConfirmed = true };
                        var createResult = await userManager.CreateAsync(devUser, devPassword);
                        if (createResult.Succeeded)
                        {
                            Console.WriteLine($"[DEV SEED] Created dev user 'user' with password: {devPassword}");
                        }
                        else
                        {
                            var errors = string.Join("; ", createResult.Errors.Select(e => e.Description));
                            Console.WriteLine("[DEV SEED] Failed to create dev user: " + errors);
                        }
                    }

                    // Ensure a deterministic smoke-test user exists so automated tests can log in
                    var smokeUser = await userManager.FindByNameAsync("smoketest");
                    if (smokeUser == null)
                    {
                        var smokePassword = "SmokeTest123!";
                        IdentityUser smoke = new() { UserName = "smoketest", Email = "smoketest@example.com", EmailConfirmed = true };
                        var smokeResult = await userManager.CreateAsync(smoke, smokePassword);
                        if (smokeResult.Succeeded)
                        {
                            Console.WriteLine($"[DEV SEED] Created smoke-test user 'smoketest' with password: {smokePassword}");
                        }
                        else
                        {
                            var errors = string.Join("; ", smokeResult.Errors.Select(e => e.Description));
                            Console.WriteLine("[DEV SEED] Failed to create smoke-test user: " + errors);
                        }
                    }

                    if (!dbContext.Project.Any())
                    {
                        Project project = new Project();
                        project.ProjectName = "Projekt #1";
                        project.Start = DateTime.UtcNow.Date;
                        project.Deadline = DateTime.UtcNow.Date.AddDays(30);
                        project.Budget = 100000;
                        project.HourlyRate = 100;
                        project.Team = "Team #1";

                        dbContext.Project.Add(project);
                        await dbContext.SaveChangesAsync();
                    }

                        if (!dbContext.Tasks.Any())
                        {
                            Tasks tasks = new Tasks();
                            tasks.Title = "Task";
                            tasks.TaskStart = DateTime.UtcNow.Date;
                            tasks.ExpectedTime = TimeSpan.FromHours(8);
                            tasks.InCharge = "John";
                            tasks.Description = "Describe your task";
                            tasks.WorkDone = true;

                            var seedProjectId = dbContext.Project.Select(p => p.Id).OrderBy(id => id).FirstOrDefault();

                            if (seedProjectId == 0)
                            {
                                // No project exists yet; create a default project so the Task can reference it.
                                var defaultProject = new Project
                                {
                                    ProjectName = "Default Project (seed)",
                                    Start = DateTime.UtcNow.Date,
                                    Deadline = DateTime.UtcNow.Date.AddDays(30),
                                    Budget = 1000,
                                    HourlyRate = 100
                                };
                                dbContext.Project.Add(defaultProject);
                                await dbContext.SaveChangesAsync();
                                seedProjectId = defaultProject.Id;
                            }

                            tasks.ProjectId = seedProjectId;

                            dbContext.Tasks.Add(tasks);
                            await dbContext.SaveChangesAsync();
                        }

                    if (!dbContext.User.Any())
                    {
                        
                        var identityUser = await userManager.FindByNameAsync("user");
                        if (identityUser != null)
                        {
                            User dataUser = new User();
                            dataUser.Name = identityUser.UserName ?? "user";
                            dataUser.Email = identityUser.Email ?? "user@example.com";
                            dataUser.Password = "[identity-managed]";
                            // Ensure CreatedAt is set (migration requires non-null CreatedAt)
                            dataUser.CreatedAt = DateTime.UtcNow;
                            dbContext.User.Add(dataUser);
                            await dbContext.SaveChangesAsync();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Log seed errors but don't crash the app on startup in development
                    Console.WriteLine("[DEV SEED] Seed or migration step failed: " + ex.Message);
                }
            }
#endif

            app.Run();
        }
    }
}
