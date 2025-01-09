using @switch.domain.Entities;
using @switch.infrastructure.DAL;
using Xunit;

namespace Switch.Infrastructure.Tests
{
    public class RepositoryTests
    {
        private readonly Repository<BaseEntity<Guid>> _repository;

        public RepositoryTests()
        {
            _repository = new Repository<BaseEntity<Guid>>();
        }

        [Fact]
        public async Task AddAsync_ShouldAddEntity()
        {
            var entity = new BaseEntity<Guid> { Id = Guid.NewGuid(), CreatedBy = "Test" };

            await _repository.AddAsync(entity);
            var result = await _repository.GetByIdAsync(entity.Id);

            Assert.NotNull(result);
            Assert.Equal(entity.Id, result?.Id);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllEntities()
        {
            var entity1 = new BaseEntity<Guid> { Id = Guid.NewGuid(), CreatedBy = "Test1" };
            var entity2 = new BaseEntity<Guid> { Id = Guid.NewGuid(), CreatedBy = "Test2" };

            await _repository.AddAsync(entity1);
            await _repository.AddAsync(entity2);

            var result = await _repository.GetAllAsync();
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveEntity()
        {
            var entity = new BaseEntity<Guid> { Id = Guid.NewGuid(), CreatedBy = "Test" };

            await _repository.AddAsync(entity);
            await _repository.DeleteAsync(entity.Id);

            var result = await _repository.GetByIdAsync(entity.Id);
            Assert.Null(result);
        }
    }
}