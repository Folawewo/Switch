using @System.Collections.Concurrent;
using @switch.domain.Entities;
using @switch.domain.Repository;

namespace @switch.infrastructure.DAL
{
    public class InMemoryFeatureToggleRepository : IRepository<SwitchToggle>
    {
        private readonly ConcurrentDictionary<Guid, SwitchToggle> _store = new();

        public Task<IEnumerable<SwitchToggle>> GetAllAsync()
        {
            return Task.FromResult(_store.Values.AsEnumerable());
        }

        public Task<SwitchToggle?> GetByIdAsync(Guid id)
        {
            _store.TryGetValue(id, out var toggle);
            return Task.FromResult(toggle);
        }

        public Task AddAsync(SwitchToggle toggle)
        {
            _store[toggle.Id] = toggle;
            return Task.CompletedTask;
        }

        public Task UpdateAsync(SwitchToggle toggle)
        {
            _store[toggle.Id] = toggle;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            _store.TryRemove(id, out _);
            return Task.CompletedTask;
        }
    }
}