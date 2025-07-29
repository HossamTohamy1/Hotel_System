using AutoMapper;
using Hotel.Core.Entities;
using Hotel.Infrastructure.Helpers;
using Hotel.Services.DTOs;
using Hotel.Infrastructure.Presistance.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.DTOs.Auth;
using Microsoft.AspNetCore.Identity;

namespace HotelSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PasswordHasher<User> _passwordHasher = new();

        private readonly HotelDbContext _context;
        IMapper _mapper;
        public AuthController(HotelDbContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationDTO request)
        {
            if (_context.Users.Any(u => u.Username == request.Username))
                return BadRequest("Username already exists");

            var user = _mapper.Map<User>(request);

            user.PasswordHash = _passwordHasher.HashPassword(user, request.PasswordHash);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("Registered successfully");
        }


        [HttpPost("login")]
        [AllowAnonymous]

        public IActionResult Login(LoginDTO request)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null)
                return Unauthorized("Invalid username or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.PasswordHash);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid username or password");

            var token = GenerateToken.Generate(user.Id, user.Username, user.Role);
            return Ok(new { token });
        }

    }
}
