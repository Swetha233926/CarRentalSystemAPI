using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalSystemAPI.Data;
using CarRentalSystemAPI.Models;
using System.Threading.Tasks;
using System.Linq;
using CarRentalSystemAPI.Services;

namespace CarRentalSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CarDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(CarDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        // Register a new user
        [HttpPost("register")]
        public async Task<IActionResult> Register(User user)
        {
            // Check if email already exists
            var existingUser = await _context.users.FirstOrDefaultAsync(u => u.User_email == user.User_email);
            if (existingUser != null)
            {
                return BadRequest("User with this email already exists.");
            }

            // Hash password (for simplicity, not hashed here; hash in production)
            user.User_password = user.User_password; // Use hashing here, e.g., BCrypt
            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }

        // Login and generate JWT token
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Authenticate user
            var user = await _context.users.FirstOrDefaultAsync(u =>
                u.User_email == loginDto.Email && u.User_password == loginDto.Password);

            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }

            // Generate JWT token
            var token = _jwtService.GenerateToken(user.User_email, user.User_role);

            return Ok(new { Token = token });
        }
    }

    // DTO for login request
    public class LoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
