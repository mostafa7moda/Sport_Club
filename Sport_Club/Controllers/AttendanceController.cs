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
    [Authorize(Roles = "Admin,Manager,Trainer")] // Adjusted roles
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceService _attendanceService;

        public AttendanceController(IAttendanceService attendanceService)
        {
            _attendanceService = attendanceService;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _attendanceService.GetAllAsync();
            return Ok(records);
        }

        // GET BY MEMBER
        [HttpGet("member/{memberId}")]
        public async Task<IActionResult> GetByMember(int memberId)
        {
            var records = await _attendanceService.GetByMemberIdAsync(memberId);
            return Ok(records);
        }

         // GET BY SECTION
        [HttpGet("section/{sectionId}")]
        public async Task<IActionResult> GetBySection(int sectionId)
        {
            var records = await _attendanceService.GetBySectionIdAsync(sectionId);
            return Ok(records);
        }

        // LOG ATTENDANCE
        [HttpPost]
        public async Task<IActionResult> Log(AttendanceLogDto dto)
        {
            var created = await _attendanceService.LogAsync(dto);
            return CreatedAtAction(nameof(GetAll), null, created); // URL to specific item ideally, but GetAll works for now
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
               await _attendanceService.DeleteAsync(id);
               return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
