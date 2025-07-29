using Hotel.Infrastructure.Presistance.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelSystem.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public UsersController(HotelDbContext context)
        {
            _context = context;
        }
        [Authorize(Policy = "Admin")]

        [HttpPut("changerole/{userId}")]
        public async Task<IActionResult> ChangeRole(int userId, [FromBody] string role)
        {
            var user = await _context.Users
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound();



            _context.Users.Update(user);


            return Ok(new
            {
                message = "Role updated successfully",
                userId,
                newRole = role
            });

        }

        [Authorize(Policy = "Receptionist")]
        [HttpGet("reception-area")]
        public IActionResult ReceptionOnly() => Ok("Welcome Receptionist!");

        [Authorize(Policy = "Guest")]
        [HttpGet("guest-area")]
        public IActionResult GuestOnly() => Ok("Welcome Guest!");

    }
}
