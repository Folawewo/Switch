using Moq;
using @switch.domain.Entities;
using @switch.application.Implementation;
using @switch.domain.Repository;
using Xunit;

namespace Switch.Application.Tests
{
    public class SwitchToggleServiceTests
    {
        private readonly Mock<IRepository<SwitchToggle>> _repositoryMock;
        private readonly SwitchToggleService _service;

        public SwitchToggleServiceTests()
        {
            _repositoryMock = new Mock<IRepository<SwitchToggle>>();
            _service = new SwitchToggleService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllTogglesAsync_ShouldReturnAllToggles()
        {
            var toggles = new List<SwitchToggle>
            {
                new SwitchToggle { Id = Guid.NewGuid(), Name = "Feature1", IsEnabled = true },
                new SwitchToggle { Id = Guid.NewGuid(), Name = "Feature2", IsEnabled = false }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(toggles);

            var result = await _service.GetAllTogglesAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task CreateToggleAsync_ShouldAddToggle()
        {
            var toggle = new SwitchToggle { Name = "Feature1", IsEnabled = true };

            await _service.CreateToggleAsync(toggle);

            _repositoryMock.Verify(repo => repo.AddAsync(It.IsAny<SwitchToggle>()), Times.Once);
        }

        [Fact]
        public async Task UpdateToggleAsync_ShouldUpdateToggle()
        {
            var toggle = new SwitchToggle { Id = Guid.NewGuid(), Name = "Feature1", IsEnabled = true };

            _repositoryMock.Setup(repo => repo.GetByIdAsync(toggle.Id)).ReturnsAsync(toggle);

            toggle.Name = "UpdatedFeature";
            await _service.UpdateToggleAsync(toggle);

            _repositoryMock.Verify(repo => repo.UpdateAsync(toggle), Times.Once);
        }

        [Fact]
        public async Task EvaluateToggleAsync_ShouldReturnCorrectState()
        {
            var toggles = new List<SwitchToggle>
            {
                new SwitchToggle { Name = "Feature1", IsEnabled = true }
            };

            _repositoryMock.Setup(repo => repo.GetAllAsync()).ReturnsAsync(toggles);

            var result = await _service.EvaluateToggleAsync("Feature1", new Dictionary<string, object>());

            Assert.True(result);
        }
    }
}
