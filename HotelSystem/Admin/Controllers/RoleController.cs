using Hotel.Core.Entities;
using Hotel.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleServices _roleService;

        public RoleController(RoleServices roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("get-roles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _roleService.GetAll();
            if (result == null || !result.Any())
            {
                return NotFound(new { message = "No roles found." });
            }
            return Ok(result);
        }

        [HttpPost("add-role")]
        public async Task<IActionResult> AddRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest(new { message = "Role name cannot be empty." });
            }


            var newRole = new Role
            {
                Name = roleName
            };

            var result = await _roleService.AddAsync(newRole);
            if (result)
            {
                return Ok(new { message = "Role added successfully." });
            }
            return BadRequest(new { message = "Failed to add role." });
        }

        [HttpPut("update-role/{id}")]
        public async Task<IActionResult> UpdateRole(int id, [FromBody] string newRoleName)
        {
            if (string.IsNullOrWhiteSpace(newRoleName))
            {
                return BadRequest(new { message = "Role name cannot be empty." });
            }

            var updatedRole = new Role
            {
                Id = id,
                Name = newRoleName
            };

            var result = await _roleService.UpdateAsync(updatedRole);
            if (result)
            {
                return Ok(new { message = "Role updated successfully." });
            }
            return NotFound(new { message = "Role not found." });
        }

        [HttpDelete("delete-role/{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var result = await _roleService.DeleteAsync(id);
            if (result)
            {
                return Ok(new { message = "Role deleted successfully." });
            }
            return NotFound(new { message = "Role not found." });
        }
    }
}
