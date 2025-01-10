using Microsoft.AspNetCore.SignalR;
using @switch.infrastructure.Hubs; 
using @switch.domain.Entities;

namespace @switch.infrastructure.Services
{
    public class SwitchToggleNotifier
    {
        private readonly IHubContext<SwitchHub> _hubContext;

        public SwitchToggleNotifier(IHubContext<SwitchHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyToggleCreated(SwitchToggle toggle)
        {
            await _hubContext.Clients.All.SendAsync("ToggleCreated", toggle);
        }

        public async Task NotifyToggleUpdated(SwitchToggle toggle)
        {
            await _hubContext.Clients.All.SendAsync("ToggleUpdated", toggle);
        }

        public async Task NotifyToggleDeleted(Guid id)
        {
            await _hubContext.Clients.All.SendAsync("ToggleDeleted", id);
        }
    }
}