using Hotel.Services.DTOs.Feature_Role;
using Hotel.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleFeatureController : ControllerBase
    {
        private readonly RoleFeatureService _roleFeatureService;

        public RoleFeatureController(RoleFeatureService roleFeatureService)
        {
            _roleFeatureService = roleFeatureService;
        }
      

        [HttpPost("assign")]
        public async Task<IActionResult> AssignFeature([FromBody] AssignFeatureDTO dto)
        {
            try
            {
                await _roleFeatureService.AssignFeatureToRole(dto.RoleName, dto.FeatureName);
                return Ok("Feature assigned to role successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("update")]
        public IActionResult UpdateFeature([FromBody] UpdateFeatureDTO dto)
        {
            try
            {
                _roleFeatureService.UpdateFeatureForRole(dto.RoleName, dto.OldFeatureName, dto.NewFeatureName);
                return Ok("Feature updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _roleFeatureService.Delete(id);
                if (!deleted) return NotFound("Not found.");

                return Ok("Feature removed from role.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

