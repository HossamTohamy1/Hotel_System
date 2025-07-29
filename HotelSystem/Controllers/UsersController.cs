using Hotel.Infrastructure.Presistance.Data;
using Hotel.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace HotelSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HotelDbContext _context;
        private readonly UserService _userService;
        private readonly ILogger<UsersController> _logger;
        public UsersController(HotelDbContext context, UserService userService, ILogger<UsersController> logger)
        {
            _context = context;
            _userService = userService;
            _logger = logger;
        }


        [Authorize(Policy = "Staff")]
        [HttpGet("reception-area")]
        public IActionResult ReceptionOnly()
        {
            _logger.LogInformation("ReceptionOnly endpoint called");
            return Ok("Welcome Receptionist!");
        }

        [Authorize(Policy = "Customer")]
        [HttpGet("guest-area")]
        public IActionResult GuestOnly()
        {
            _logger.LogInformation("GuestOnly endpoint called");
            return Ok("Welcome Guest!");
        }


        [Authorize(Policy = "Admin")]

        [HttpPut("changerole/{userId}")]
        public async Task<IActionResult> ChangeRole(int userId, [FromBody] string role)
        {
            _logger.LogInformation("ChangeRole endpoint called for userId: {UserId}, new role: {Role}", userId, role);
            var user = await _context.Users
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                _logger.LogWarning("ChangeRole failed: User not found for userId: {UserId}", userId);
                return NotFound();
            }
            _context.Users.Update(user);
            _logger.LogInformation("Role updated successfully for userId: {UserId} to role: {Role}", userId, role);
            return Ok(new
            {
                message = "Role updated successfully",
                userId,
                newRole = role
            });
        }

       


        [HttpPost("promote-to-staff")]
        public async Task<IActionResult> PromoteCustomerToStaff([FromBody] CustomerToStaffPromotionDto promotionDto)
        {
            _logger.LogInformation("PromoteCustomerToStaff endpoint called for customerId: {CustomerId}", promotionDto.CustomerId);
            try
            {
                var result = await _userService.PromoteCustomerToStaffAsync(promotionDto);
                _logger.LogInformation("Customer promoted to staff successfully: {CustomerId}", promotionDto.CustomerId);
                return Ok(new { success = true, message = "Customer successfully promoted to staff" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error promoting customer to staff: {CustomerId}", promotionDto.CustomerId);
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }


}
