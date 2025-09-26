using KooliProjekt.Controllers;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace KooliProjekt.UnitTests.ServiceTests
{
    public class ProjectServiceTests
    {
        private readonly Mock<IProjectService> _projectServiceMock;
        private readonly Mock<IFileClient> _fileClientMock;
        private readonly ProjectController _controller;

        public ProjectServiceTests()
        {
            _projectServiceMock = new Mock<IProjectService>();
            _fileClientMock = new Mock<IFileClient>();
            _controller = new ProjectController(_projectServiceMock.Object, _fileClientMock.Object);
        }


        [Fact]
        public async Task Index_ReturnsViewResult_WithAListOfProjects()
        {
            // Arrange
            var projects = new List<Project> { new Project { Id = 1, ProjectName = "Test Project" } };
            _projectServiceMock.Setup(service => service.List()).ReturnsAsync(projects);
            _fileClientMock.Setup(client => client.List(FileStoreNames.Images)).Returns(new string[] { "image1.jpg", "image2.jpg" });

            // Act
            var result = await _controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<Project>>(viewResult.ViewData.Model);
            Assert.Equal(1, model.Count());
        }

        [Fact]
        public async Task Details_ReturnsViewResult_WithProject()
        {
            // Arrange
            var project = new Project { Id = 1, ProjectName = "Test Project" };
            _projectServiceMock.Setup(service => service.GetById(1)).ReturnsAsync(project);

            // Act
            var result = await _controller.Details(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Project>(viewResult.ViewData.Model);
            Assert.Equal("Test Project", model.ProjectName);
        }

        [Fact]
        public async Task Create_Post_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var project = new Project { ProjectName = "New Project" };
            var files = new List<IFormFile>();
            var fileMock = new Mock<IFormFile>();
            fileMock.Setup(_ => _.FileName).Returns("testfile.jpg");
            fileMock.Setup(_ => _.OpenReadStream()).Returns(new MemoryStream());
            files.Add(fileMock.Object);

            // Act
            var result = await _controller.Create(project, files.ToArray());

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task Edit_ReturnsViewResult_WithProject()
        {
            // Arrange
            var project = new Project { Id = 1, ProjectName = "Test Project" };
            _projectServiceMock.Setup(service => service.GetById(1)).ReturnsAsync(project);

            // Act
            var result = await _controller.Edit(1);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<Project>(viewResult.ViewData.Model);
            Assert.Equal("Test Project", model.ProjectName);
        }

        [Fact]
        public async Task DeleteConfirmed_RedirectsToIndex_WhenProjectIsDeleted()
        {
            // Arrange
            _projectServiceMock.Setup(service => service.Delete(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteConfirmed(1);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }
    }
}
