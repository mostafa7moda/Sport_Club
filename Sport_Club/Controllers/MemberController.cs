using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Club.DTOs;
using Sport_Club.Interfaces;
using Sport_Club.Models;

namespace Sport_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Member")]
    public class MemberController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public MemberController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        // GET ALL MEMBERS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var members = await _unitOfWork.Members.GetAllAsync();

            var result = members.Select(m => new MemberResponseDto
            {
                Id = m.UserId.ToString(),
                FullName = m.User?.FullName,
                EmergencyPhone = m.EmergencyPhone,
                HealthNotes = m.HealthNotes,
                JoinDate = m.JoinDate
            });

            return Ok(result);
        }

        // GET MEMBER BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null) return NotFound();

            var dto = new MemberResponseDto
            {
                Id = member.UserId.ToString(),
                FullName = member.User?.FullName,
                EmergencyPhone = member.EmergencyPhone,
                HealthNotes = member.HealthNotes,
                JoinDate = member.JoinDate
            };

            return Ok(dto);
        }

        // CREATE MEMBER (Admin)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(MemberDto dto)
        {
            var user = await _userManager.FindByIdAsync(dto.UserId);
            if (user == null) return BadRequest("User not found");

            var member = new Member
            {
                UserId = user.Id,
                JoinDate = dto.JoinDate,
                EmergencyPhone = dto.EmergencyPhone,
                HealthNotes = dto.HealthNotes
            };

            await _unitOfWork.Members.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { message = "Member created successfully" });
        }

        // UPDATE MEMBER
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, MemberDto dto)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null) return NotFound();

            member.EmergencyPhone = dto.EmergencyPhone;
            member.HealthNotes = dto.HealthNotes;
            member.JoinDate = dto.JoinDate;

            _unitOfWork.Members.Update(member);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { message = "Member updated successfully" });
        }

        // DELETE MEMBER
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]        
        public async Task<IActionResult> Delete(int id)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(id);
            if (member == null) return NotFound();

            _unitOfWork.Members.Delete(member);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { message = "Member deleted successfully" });
        }
    }
}
