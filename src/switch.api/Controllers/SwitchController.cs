using Microsoft.AspNetCore.Mvc;
using @switch.application.Interface;
using @switch.domain.Entities;

namespace @switch.api.Controllers
{
    [ApiController]
    [Route("api/v1/switch")]
    public class SwitchController : ControllerBase
    {
        private readonly ISwitchToggleService _switchToggleRepository;

        public SwitchController(ISwitchToggleService switchToggleRepository)
        {
            _switchToggleRepository = switchToggleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToggles() => Ok(await _switchToggleRepository.GetAllTogglesAsync());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetToggleById(Guid id)
        {
            var toggle = await _switchToggleRepository.GetToggleByIdAsync(id);
            return toggle == null ? NotFound() : Ok(toggle);
        }

        [HttpPost]
        public async Task<IActionResult> CreateToggle([FromBody] SwitchToggle toggle)
        {
            toggle.Id = Guid.NewGuid();
            await _switchToggleRepository.CreateToggleAsync(toggle);
            return CreatedAtAction(nameof(GetToggleById), new { id = toggle.Id }, toggle);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateToggle(Guid id, [FromBody] SwitchToggle toggle)
        {
            toggle.Id = id;
            await _switchToggleRepository.UpdateToggleAsync(toggle);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteToggle(Guid id)
        {
            await _switchToggleRepository.DeleteToggleAsync(id);
            return NoContent();
        }
    }
}