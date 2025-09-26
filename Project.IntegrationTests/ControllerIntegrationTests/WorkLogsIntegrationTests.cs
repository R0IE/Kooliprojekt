using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using KooliProjekt.Data;
using Microsoft.EntityFrameworkCore;
using KooliProjekt.IntegrationTests.Helpers;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace KooliProjekt.IntegrationTests.ControllerIntegrationTests
{
    public class WorkLogsIntegrationTests : TestBase
    {
        [Fact]
        public async Task Get_Create_PopulatesUsersSelect()
        {
            // Arrange
            var client = Factory.CreateClient();
            var context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));

            context.Database.EnsureCreated();
            var user = new User { Name = "Test User", Email = "test@example.com", CreatedAt = System.DateTime.UtcNow };
            context.User.Add(user);
            await context.SaveChangesAsync();

            // Act
            var response = await client.GetAsync("/WorkLogs/Create");

            // Cleanup
            context.Database.EnsureDeleted();

            // Assert
            response.EnsureSuccessStatusCode();
            var html = await response.Content.ReadAsStringAsync();
            Assert.Contains("<select", html);
            Assert.Contains("Test User", html);
        }

        [Fact]
        public async Task Post_Create_SavesWorkLogAndRedirects()
        {
            // Arrange
            var client = Factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = false });
            var context = (ApplicationDbContext)Factory.Services.GetService(typeof(ApplicationDbContext));

            context.Database.EnsureCreated();
            var user = new User { Name = "Poster", Email = "poster@example.com", CreatedAt = System.DateTime.UtcNow };
            var project = new Project { ProjectName = "P1", Start = System.DateTime.UtcNow, Deadline = System.DateTime.UtcNow.AddDays(1), Budget = 1m, HourlyRate = 1m };
            context.User.Add(user);
            context.Project.Add(project);
            await context.SaveChangesAsync();

            var form = new Dictionary<string, string>
            {
                { "Date", System.DateTime.UtcNow.ToString("yyyy-MM-dd") },
                { "TimeCost", "01:00:00" },
                { "Description", "Worked on stuff" },
                { "Performer", user.Name }
            };

            var content = new FormUrlEncodedContent(form);

            // Act
            var response = await client.PostAsync("/WorkLogs/Create", content);

            // Diagnostic: if the server didn't redirect, include the response body to help debug
            if (response.StatusCode != HttpStatusCode.Redirect)
            {
                var body = await response.Content.ReadAsStringAsync();
                System.Console.WriteLine("POST /WorkLogs/Create response body:\n" + body);
            }

            // Assert
            Assert.Equal(HttpStatusCode.Redirect, response.StatusCode);

            // verify saved
            var saved = await context.WorkLogs.FirstOrDefaultAsync(w => w.Description == "Worked on stuff");
            Assert.NotNull(saved);

            // Cleanup
            context.Database.EnsureDeleted();
        }
    }
}
