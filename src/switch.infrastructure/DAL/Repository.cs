using @switch.domain.Entities;
using @switch.domain.Repository;
using System.Collections.Concurrent;

namespace @switch.infrastructure.DAL
{
    public class Repository<T> : IRepository<T> where T : BaseEntity<Guid>
    {
        private readonly ConcurrentDictionary<Guid, T> _store = new();

        public Task<IEnumerable<T>> GetAllAsync() => Task.FromResult(_store.Values.AsEnumerable());

        public Task<T?> GetByIdAsync(Guid id) => Task.FromResult(_store.TryGetValue(id, out var entity) ? entity : null);

        public Task AddAsync(T entity)
        {
            _store[entity.Id] = entity;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(T entity)
        {
            _store[entity.Id] = entity;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _store.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}