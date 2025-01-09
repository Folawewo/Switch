using @switch.application.Interface;
using @switch.domain.Entities;
using @switch.domain.Repository;

namespace @switch.application.Implementation
{
    public class SwitchToggleService : ISwitchToggleService
    {
        private readonly IRepository<SwitchToggle> _switchToggleRepository;

        public SwitchToggleService(IRepository<SwitchToggle> switchToggleRepository)
        {
            _switchToggleRepository = switchToggleRepository;
        }

        public async Task<IEnumerable<SwitchToggle>> GetAllTogglesAsync() => await _switchToggleRepository.GetAllAsync();

        public async Task<SwitchToggle?> GetToggleByIdAsync(Guid id) => await _switchToggleRepository.GetByIdAsync(id);

        public async Task CreateToggleAsync(SwitchToggle toggle) => await _switchToggleRepository.AddAsync(toggle);

        public async Task UpdateToggleAsync(SwitchToggle toggle) => await _switchToggleRepository.UpdateAsync(toggle);

        public async Task DeleteToggleAsync(Guid id) => await _switchToggleRepository.DeleteAsync(id);

        public async Task<bool> EvaluateToggleAsync(string toggleName, Dictionary<string, object> context)
        {
            var toggles = await _switchToggleRepository.GetAllAsync();
            var toggle = toggles.FirstOrDefault(t => t.Name == toggleName && t.IsEnabled);

            if (toggle == null) return false;
            
            return true;
        }
    }
}