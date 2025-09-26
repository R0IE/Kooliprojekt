using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class TasksService_IntegrationTests
    {
        private ApplicationDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(dbName)
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Save_clamps_ExpectedTime_and_assigns_ProjectId_when_zero()
        {
            // Arrange
            using var context = CreateContext("test_db_1");
            // seed a project
            var project = new Project { ProjectName = "P1", Start = DateTime.UtcNow, Deadline = DateTime.UtcNow.AddDays(10), Budget = 100, HourlyRate = 10 };
            context.Project.Add(project);
            await context.SaveChangesAsync();

            var svc = new TasksService(context);
            var t = new Tasks
            {
                Title = "T1",
                TaskStart = DateTime.UtcNow,
                ExpectedTime = TimeSpan.FromDays(2), // > 24h should be clamped
                ProjectId = 0,
                Description = "desc",
                WorkDone = false
            };

            // Act
            await svc.Save(t);

            // Assert
            var saved = await context.Tasks.FirstOrDefaultAsync();
            Assert.NotNull(saved);
            Assert.True(saved.ExpectedTime < TimeSpan.FromDays(1));
            Assert.Equal(project.Id, saved.ProjectId);
        }
    }
}
