using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sport_Club.DTOs;
using Sport_Club.Enum;
using Sport_Club.Interfaces;
using Sport_Club.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sport_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TrainerController : ControllerBase
    {
        private readonly ITrainerService _trainerService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public TrainerController(ITrainerService trainerService, UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _trainerService = trainerService;
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost("add")] // Renamed endpoint or keep? "add" is explicit, but POST /api/trainer is RESTful.
        // Keeping "add" for compatibility or switch to REST? User asked "fix the logic... add Cruds".
        // I'll make standard CRUDs available too.
        public async Task<IActionResult> Create([FromBody] TrainerRegistrationDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _unitOfWork.BeginTransactionAsync();
            try
            {
                var user = new ApplicationUser
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    Gender = dto.Gender
                };

                var userResult = await _userManager.CreateAsync(user, dto.Password);
                if (!userResult.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return BadRequest(userResult.Errors);
                }

                await _userManager.AddToRoleAsync(user, Roles.Trainer.ToString());

                var trainerDto = new TrainerCreateDto
                {
                    UserId = user.Id,
                    Gender = dto.Gender,
                    Shift = dto.Shift,
                    ExperienceYears = dto.ExperienceYears,
                    SectionId = dto.SectionId
                };

                var createdTrainer = await _trainerService.CreateAsync(trainerDto);
                await _unitOfWork.CommitAsync();

                return CreatedAtAction(nameof(GetById), new { id = createdTrainer.Id }, createdTrainer);
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest(new { message = "Failed to add trainer", detail = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trainers = await _trainerService.GetAllAsync();
            return Ok(trainers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trainer = await _trainerService.GetByIdAsync(id);
            if (trainer == null) return NotFound();
            return Ok(trainer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, TrainerUpdateDto dto)
        {
            try
            {
                await _trainerService.UpdateAsync(id, dto);
                return Ok(new { message = "Trainer updated successfully" });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _trainerService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
