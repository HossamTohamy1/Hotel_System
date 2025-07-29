using Hotel.Core.Entities;
using Hotel.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly FeatureServices _featureService;
        public FeatureController(FeatureServices featureService)
        {
            _featureService = featureService;
        }
        [HttpGet("get-all")]
        public IActionResult Get()
        {
            var result = _featureService.GetAllFeaturesAsync();
            if (result == null || !result.Any())
            {
                return NotFound(new { message = "No features found." });
            }
            return Ok(result);
        }
        [HttpGet("get-by-id/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _featureService.GetFeatureById(id);
            if (result == null)
            {
                return NotFound(new { message = $"Feature with ID {id} not found." });
            }
            return Ok(result);
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddFeature([FromBody] string featureName)
        {
            if (string.IsNullOrWhiteSpace(featureName))
            {
                return BadRequest(new { message = "Feature name cannot be empty." });
            }

            var feature = new Feature
            {
                Name = featureName
            };

            var result = await _featureService.AddFeatureAsync(feature);
            if (result)
            {
                return CreatedAtAction(nameof(GetById), new { id = feature.Id }, new { message = "Feature added successfully." });
            }
            return BadRequest(new { message = "Failed to add feature." });
        }
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateFeature(int id, [FromBody] string featureName)
        {
            if (string.IsNullOrWhiteSpace(featureName))
            {
                return BadRequest(new { message = "Feature name cannot be empty." });
            }

            var feature = new Feature
            {
                Id = id,
                Name = featureName
            };

            var result = await _featureService.UpdateFeatureAsync(feature);
            if (result)
            {
                return Ok(new { message = "Feature updated successfully." });
            }
            return NotFound(new { message = $"Feature with ID {id} not found." });
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteFeature(int id)
        {
            var result = await _featureService.DeleteFeatureAsync(id); 
            if (result)
            {
                return Ok(new { message = "Feature deleted successfully." });
            }
            return NotFound(new { message = $"Feature with ID {id} not found." });
        }

    }
}
