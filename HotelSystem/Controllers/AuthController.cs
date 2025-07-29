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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly PasswordHasher<User> _passwordHasher = new();

        private readonly HotelDbContext _context;
        IMapper _mapper;
        private readonly ILogger<AuthController> _logger;
        public AuthController(HotelDbContext context , IMapper mapper, ILogger<AuthController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegistrationDTO request)
        {
            _logger.LogInformation("Register endpoint called for username: {Username}", request.Username);
            if (_context.Users.Any(u => u.Username == request.Username))
            {
                _logger.LogWarning("Register failed: Username already exists: {Username}", request.Username);
                return BadRequest("Username already exists");
            }

            var customer = _mapper.Map<Customer>(request.CustomerInfo);

            customer.Username = request.Username;
            customer.Email = request.Email;
            customer.Phone = request.Phone;
            customer.PasswordHash = _passwordHasher.HashPassword(customer, request.PasswordHash);
            customer.RoleId = 2; // Customer Role

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
            _logger.LogInformation("User registered successfully: {Username}", request.Username);
            return Ok("Registered successfully");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDTO request)
        {
            _logger.LogInformation("Login endpoint called for username: {Username}", request.Username);
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);

            if (user == null)
            {
                _logger.LogWarning("Login failed: Invalid username {Username}", request.Username);
                return Unauthorized("Invalid username or password");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.PasswordHash);

            if (result == PasswordVerificationResult.Failed)
            {
                _logger.LogWarning("Login failed: Invalid password for username {Username}", request.Username);
                return Unauthorized("Invalid username or password");
            }

            var roleName = _context.Roles
                .Where(r => r.Id == user.RoleId)
                .Select(r => r.Name)
                .FirstOrDefault();

            var token = GenerateToken.Generate(user.Id, user.Username, user.RoleId, roleName);
            _logger.LogInformation("Login successful for username: {Username}", request.Username);
            return Ok(new { token });
        }



    }
}
