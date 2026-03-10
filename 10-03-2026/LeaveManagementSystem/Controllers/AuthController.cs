using LeaveManagementAPI.Data;
using LeaveManagementAPI.DTOs;
using LeaveManagementAPI.Models;
using LeaveManagementAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(AppDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterDTO dto)
        {
            if (_context.Users.Any(u => u.Username == dto.Username))
                return BadRequest("Username already exists");

            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                Role = dto.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u =>
                u.Username == dto.Username && u.Password == dto.Password);

            if (user == null)
                return Unauthorized("Invalid username or password");

            var token = _tokenService.CreateToken(user);

            return Ok(new { token, role = user.Role, username = user.Username });
        }
    }
}