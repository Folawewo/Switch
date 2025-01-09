using @switch.domain.Entities;

namespace @switch.application.Interface
{
    public interface ISwitchToggleService
    {
        Task<IEnumerable<SwitchToggle>> GetAllTogglesAsync();
        Task<SwitchToggle?> GetToggleByIdAsync(Guid id);
        Task CreateToggleAsync(SwitchToggle toggle);
        Task UpdateToggleAsync(SwitchToggle toggle);
        Task DeleteToggleAsync(Guid id);
        Task<bool> EvaluateToggleAsync(string toggleName, Dictionary<string, object> context);
    }
}