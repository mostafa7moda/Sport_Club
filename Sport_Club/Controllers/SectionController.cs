using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Club.DTOs;
using Sport_Club.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Manager")] // Adjusted based on common roles
    public class SectionController : ControllerBase
    {
        private readonly ISectionService _sectionService;

        public SectionController(ISectionService sectionService)
        {
            _sectionService = sectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sections = await _sectionService.GetAllAsync();
            return Ok(sections);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var section = await _sectionService.GetByIdAsync(id);
            if (section == null) return NotFound();
            return Ok(section);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(SectionCreateDto dto)
        {
            var createdSection = await _sectionService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdSection.Id }, createdSection);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, SectionUpdateDto dto)
        {
            try
            {
                await _sectionService.UpdateAsync(id, dto);
                return Ok(new { message = "Section updated successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sectionService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
