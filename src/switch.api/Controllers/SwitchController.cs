using Microsoft.AspNetCore.Mvc;
using @switch.application.Interface;
using @switch.domain.Entities;
using @switch.infrastructure.Services;

namespace @switch.api.Controllers
{
    [ApiController]
    [Route("api/v1/switch")]
    public class SwitchController : ControllerBase
    {
        private readonly ISwitchToggleService _switchToggleService;
        private readonly SwitchToggleNotifier _notifier;

        public SwitchController(ISwitchToggleService switchToggleService, SwitchToggleNotifier notifier)
        {
            _switchToggleService = switchToggleService;
            _notifier = notifier;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToggles()
        {
            var toggles = await _switchToggleService.GetAllTogglesAsync();
            return Ok(toggles);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetToggleById(Guid id)
        {
            var toggle = await _switchToggleService.GetToggleByIdAsync(id);
            return toggle == null ? NotFound() : Ok(toggle);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToggle([FromBody] SwitchToggle toggle)
        {
            toggle.Id = Guid.NewGuid();
            await _switchToggleService.CreateToggleAsync(toggle);
            await _notifier.NotifyToggleCreated(toggle); 
            return CreatedAtAction(nameof(GetToggleById), new { id = toggle.Id }, toggle);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateToggle(Guid id, [FromBody] SwitchToggle toggle)
        {
            toggle.Id = id;
            await _switchToggleService.UpdateToggleAsync(toggle);
            await _notifier.NotifyToggleUpdated(toggle);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteToggle(Guid id)
        {
            await _switchToggleService.DeleteToggleAsync(id);
            await _notifier.NotifyToggleDeleted(id); 
            return NoContent();
        }
    }
}
