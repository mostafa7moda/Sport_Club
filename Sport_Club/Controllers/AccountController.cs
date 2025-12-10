using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sport_Club.Enum;
using Sport_Club.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sport_Club.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            IConfiguration config,
            IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _config = config;
            _unitOfWork = unitOfWork;
        }

        // REGISTER (MEMBER ONLY)
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
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

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (!result.Succeeded)
                {
                    await _unitOfWork.RollbackAsync();
                    return BadRequest(result.Errors);
                }

                // Ensure role exists (seed should already do this)
                if (!await _userManager.IsInRoleAsync(user, Roles.Member.ToString()))
                {
                    await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
                }
                else
                {
                    // still ensure the role assignment (safe)
                    await _userManager.AddToRoleAsync(user, Roles.Member.ToString());
                }

                // Create Member profile
                var member = new Member
                {
                    UserId = user.Id,
                    JoinDate = DateTime.UtcNow,
                    EmergencyPhone = dto.EmergencyPhone,
                    HealthNotes = dto.HealthNotes
                };

                await _unitOfWork.Members.AddAsync(member);
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();

                return Ok(new { message = "Member Registered Successfully" });
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                return BadRequest(new { message = "Registration Failed", detail = ex.Message });
            }
        }

        // LOGIN (JWT + CLAIMS + ROLES)
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Unauthorized("Invalid email or password.");

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized("Invalid email or password.");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.UtcNow.AddDays(7);

            var token = new JwtSecurityToken(
                issuer: _config["JWT:ValidIssuer"],
                audience: _config["JWT:ValidAudience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = tokenString,
                expiration = expires,
                user = user.UserName,
                roles = roles
            });
        }
    }
}
