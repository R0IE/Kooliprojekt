using KooliProjekt.Controllers;
using KooliProjekt.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class ProjectControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task Index_ReturnsViewResult_WithPagedProjects()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new ProjectController(context);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public async Task Create_ValidProject_RedirectsToIndex()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new ProjectController(context);
            var project = new Project
            {
                ProjectName = "Test Project",
                Start = DateTime.Today,
                Deadline = DateTime.Today.AddMonths(1),
                Budget = 10000,
                HourlyRate = 50
            };

            // Act
            var result = await controller.Create(project);

            // Assert
            var redirectResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectResult.ActionName);
        }

        [Fact]
        public async Task Details_ExistingProject_ReturnsViewWithProject()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var project = new Project
            {
                ProjectName = "Test Project",
                Start = DateTime.Today,
                Deadline = DateTime.Today.AddMonths(1),
                Budget = 10000,
                HourlyRate = 50
            };
            context.Project.Add(project);
            await context.SaveChangesAsync();

            var controller = new ProjectController(context);

            // Act
            var result = await controller.Details(project.Id);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<Project>(viewResult.Model);
            Assert.Equal(project.ProjectName, model.ProjectName);
        }

        [Fact]
        public async Task Details_NonExistingProject_ReturnsNotFound()
        {
            // Arrange
            using var context = GetInMemoryDbContext();
            var controller = new ProjectController(context);

            // Act
            var result = await controller.Details(999);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}