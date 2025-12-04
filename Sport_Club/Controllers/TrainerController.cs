using Microsoft.AspNetCore.Mvc;
using Sport_Club.DTOs;
using Sport_Club.Interfaces;

namespace Sport_Club.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _service;

        public TrainerController(ITrainerService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trainer = await _service.GetByIdAsync(id);
            if (trainer == null) return NotFound();
            return Ok(trainer);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TrainerDto dto)
        {
            var trainer = await _service.CreateAsync(dto);
            return Ok(trainer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TrainerUpdateDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
