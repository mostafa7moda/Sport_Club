using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sport_Club.Data;
using Sport_Club.DTOs;
using Sport_Club.Enum;
using Sport_Club.Interfaces;
using Sport_Club.Models;

namespace Sport_Club.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class TrainerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppDbContext _context;

        public TrainerController(
            UserManager<ApplicationUser> userManager,
            IUnitOfWork unitOfWork,
            AppDbContext context)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddTrainer([FromBody] AddTrainerDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

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

                var trainer = new Trainer
                {
                    UserId = user.Id,
                    Gender = dto.Gender,
                    Shift = dto.Shift,
                    ExperienceYears = dto.ExperienceYears,
                    SectionId = dto.SectionId

                };

                await _unitOfWork.Trainers.AddAsync(trainer);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return Ok(new
                {
                    Message = "Trainer added successfully",
                    TrainerId = trainer.ID,
                    UserId = user.Id
                });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest(new { message = "Failed to add trainer", detail = ex.Message });
            }
        }
    }
}
