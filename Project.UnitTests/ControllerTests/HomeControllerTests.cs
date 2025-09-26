using KooliProjekt.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace KooliProjekt.UnitTests.ControllerTests
{
    public class HomeControllerTests
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HomeController _controller;

        public HomeControllerTests() {
            _logger = Mock.Of<ILogger<HomeController>>();
            _controller = new HomeController(_logger);
        }

        [Fact]
        public void Index_should_return_index_view()
        {
            var indexView = _controller.Index();

            Assert.IsType<ViewResult>(indexView);
        }

        [Fact]
        public void Index_with_files_should_redirect_to_action_with_nameof_index()
        {
            IFormFile[] files = { };

            IActionResult result = _controller.Index(files);

            Assert.IsType<RedirectToActionResult>(result);
        }

        [Fact]
        public void Privacy_should_return_privacy_view()
        {
            var privacyView = _controller.Privacy();

            Assert.IsType<ViewResult>(privacyView);
        }
    }
}