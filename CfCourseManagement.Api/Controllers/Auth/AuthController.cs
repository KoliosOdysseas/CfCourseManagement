using CfCourseManagement.Api.Data;
using CfCourseManagement.Api.Dtos.Auth;
using CfCourseManagement.Api.Models;
using CfCourseManagement.Api.Services.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CfCourseManagement.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(ApplicationDbContext context, IJwtTokenService jwtTokenService)
        {
            _context = context;
            _jwtTokenService = jwtTokenService;
        }

        // POST: /api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            if (dto == null)
                return BadRequest("Register data is required.");

            if (string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("UserName and Password are required.");

            // (προ-έλεγχος) αν υπάρχει ήδη username
            var usernameTaken = await _context.Users
                .AsNoTracking()
                .AnyAsync(u => u.UserName == dto.UserName);

            if (usernameTaken)
                return Conflict("UserName is already taken.");

            // βρίσκουμε role από το RoleName
            var role = await _context.Roles
                .FirstOrDefaultAsync(r => r.Name == dto.RoleName);

            if (role == null)
                return BadRequest($"Role '{dto.RoleName}' does not exist.");

            // hash password
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // φτιάχνουμε user
            var user = new User
            {
                UserName = dto.UserName,
                PasswordHash = passwordHash,
                Email = dto.Email,
                IsActive = true,
                RoleId = role.Id
            };

            // SAVE με try/catch (UNIQUE constraint / race condition)
            if (dto.RoleName == "Student")
            {
                var student = new Student
                {
                    FullName = dto.UserName,
                    Email = dto.Email
                };
                try
                {
                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    // αν δύο register έρθουν ταυτόχρονα με ίδιο username → UNIQUE index σκάει
                    return Conflict("UserName already exists.");
                }
            }
            else if (dto.RoleName == "Teacher")
            {
                var teacher = new Teacher
                {
                    FullName = dto.UserName,
                    Email = dto.Email
                };
                try
                {
                    _context.Teachers.Add(teacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    // αν δύο register έρθουν ταυτόχρονα με ίδιο username → UNIQUE index σκάει
                    return Conflict("UserName already exists.");
                }
            }
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                // αν δύο register έρθουν ταυτόχρονα με ίδιο username → UNIQUE index σκάει
                return Conflict("UserName already exists.");
            }

            // reload user με Role (για να υπάρχει user.Role.Name όταν φτιάχνουμε token)
            var createdUser = await _context.Users
                .Include(u => u.Role)
                .FirstAsync(u => u.Id == user.Id);

            // κόβουμε token
            var (token, expiresAtUtc) = _jwtTokenService.CreateToken(createdUser);

            // response
            var response = new AuthResponseDto
            {
                Token = token,
                ExpiresAtUtc = expiresAtUtc,
                UserName = createdUser.UserName,
                Role = createdUser.Role.Name
            };

            return Ok(response);
        }

        // POST: /api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            if (dto == null)
                return BadRequest("Login data is required.");

            if (string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("UserName and Password are required.");

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserName == dto.UserName);

            if (user == null)
                return Unauthorized("Invalid credentials.");

            if (!user.IsActive)
                return Unauthorized("User is inactive.");

            var passwordOk = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!passwordOk)
                return Unauthorized("Invalid credentials.");

            var (token, expiresAtUtc) = _jwtTokenService.CreateToken(user);

            var response = new AuthResponseDto
            {
                Token = token,
                ExpiresAtUtc = expiresAtUtc,
                UserName = user.UserName,
                Role = user.Role.Name
            };

            return Ok(response);
        }
    }
}
