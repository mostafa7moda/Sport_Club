using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sport_Club.DTOs;
using Sport_Club.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin,Member")]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET ALL MEMBERS
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var members = await _memberService.GetAllAsync();
            return Ok(members);
        }

        // GET MEMBER BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var member = await _memberService.GetByIdAsync(id);
            if (member == null) return NotFound();
            return Ok(member);
        }

        // CREATE MEMBER (Admin)
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(MemberCreateDto dto)
        {
            try
            {
                var createdMember = await _memberService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdMember.Id }, createdMember);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // UPDATE MEMBER
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, MemberUpdateDto dto)
        {
            try
            {
                await _memberService.UpdateAsync(id, dto);
                return Ok(new { message = "Member updated successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        // DELETE MEMBER
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _memberService.DeleteAsync(id);
                return NoContent(); // 204 No Content for successful delete
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
