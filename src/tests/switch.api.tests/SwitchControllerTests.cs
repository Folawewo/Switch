using Moq;
using Microsoft.AspNetCore.Mvc;
using @switch.api.Controllers;
using @switch.application.Interface;
using @switch.domain.Entities;
using @switch.infrastructure.Services;
using Xunit;

namespace Switch.Api.Tests
{
    public class SwitchControllerTests
    {
        private readonly Mock<ISwitchToggleService> _serviceMock;
        private readonly Mock<SwitchToggleNotifier> _notifierMock;
        private readonly SwitchController _controller;

        public SwitchControllerTests()
        {
            _serviceMock = new Mock<ISwitchToggleService>();
            _notifierMock = new Mock<SwitchToggleNotifier>();
            _controller = new SwitchController(_serviceMock.Object, _notifierMock.Object);
        }

        [Fact]
        public async Task GetAllToggles_ShouldReturnOkResult()
        {
            _serviceMock.Setup(service => service.GetAllTogglesAsync())
                .ReturnsAsync(new List<SwitchToggle> { new SwitchToggle { Name = "Feature1" } });

            var result = await _controller.GetAllToggles() as OkObjectResult;

            Assert.NotNull(result);
            Assert.IsType<List<SwitchToggle>>(result.Value);
        }

        [Fact]
        public async Task GetToggleById_ShouldReturnNotFound_WhenToggleDoesNotExist()
        {
            _serviceMock.Setup(service => service.GetToggleByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync((SwitchToggle)null);

            var result = await _controller.GetToggleById(Guid.NewGuid());

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task CreateToggle_ShouldReturnCreatedAtAction()
        {
            var toggle = new SwitchToggle { Name = "Feature1" };

            var result = await _controller.CreateToggle(toggle) as CreatedAtActionResult;

            Assert.NotNull(result);
            Assert.Equal(nameof(_controller.GetToggleById), result.ActionName);
        }

        [Fact]
        public async Task DeleteToggle_ShouldReturnNoContent()
        {
            var result = await _controller.DeleteToggle(Guid.NewGuid());

            Assert.IsType<NoContentResult>(result);
        }
    }
}
