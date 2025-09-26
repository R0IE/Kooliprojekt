using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class TasksServiceTests
    {
        private readonly TasksController _controller;
        private readonly Mock<ITasksService> _mockTasksService;

        public TasksServiceTests()
        {
            _mockTasksService = new Mock<ITasksService>();
            _controller = new TasksController(_mockTasksService.Object);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_id_is_missing()
        {
            // Arrange
            int? id = null;

            // Act
            var result = await _controller.Details(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Details_should_return_notfound_when_task_is_not_found()
        {
            // Arrange
            Tasks task = null;
            int id = 4;
            _mockTasksService.Setup(srv => srv.GetById(id))
                             .ReturnsAsync(task)
                             .Verifiable();

            // Act
            var result = await _controller.Details(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
            _mockTasksService.VerifyAll();
        }

        [Fact]
        public async Task Details_should_return_details_view_when_task_exists()
        {
            // Arrange
            int id = 4;
            Tasks task = new Tasks { Id = id, Title = "Task Title" };
            _mockTasksService.Setup(srv => srv.GetById(id))
                             .ReturnsAsync(task)
                             .Verifiable();

            // Act
            var result = await _controller.Details(id) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.True(result.ViewName == "Details" || string.IsNullOrEmpty(result.ViewName));
            Assert.Equal(task, result.Model);
            _mockTasksService.VerifyAll();
        }
    }
}
