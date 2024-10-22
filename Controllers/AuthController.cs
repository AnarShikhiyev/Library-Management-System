using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectLibrary_Back.DTO;
using ProjectLibrary_Back.Interfaces;
using ProjectLibrary_Back.Models;
using ProjectLibrary_Back.Services;

namespace ProjectLibrary_Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserAuthService _userAuthService;
        private readonly AuthService _authService;
        private readonly LibraryProjectContext _context; 
        public AuthController(IUserAuthService userAuthService, AuthService authService, LibraryProjectContext context)
        {
            _userAuthService = userAuthService;
            _authService = authService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerDto)
        {
            var result = await _userAuthService.RegisterUserAsync(registerDto);

            if (result)
            {
                return Ok("İstifadəçi uğurla qeydiyyatdan keçdi.");
            }
            return BadRequest("Qeydiyyat zamanı xəta baş verdi. İstifadəçi adı mövcuddur.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var userId = await _userAuthService.LoginAsync(loginDto);

            if (userId == null)
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

   
            var user = await _context.Users.FindAsync(int.Parse(userId));

       
            if (user == null)
            {
                return Unauthorized(new { message = "User not found." });
            }

            var token = _authService.GenerateJwtToken(user);

       
            return Ok(new { Token = token });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromBody] int userId)
        {
            var result = await _userAuthService.LogoutAsync(userId);

            if (result)
            {
                return Ok("İstifadəçi uğurla çıxış etdi.");
            }

            return BadRequest("Çıxış zamanı xəta baş verdi. İstifadəçi tapılmadı.");
        }
    }
}
