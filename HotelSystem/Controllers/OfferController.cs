using Hotel.Core.Entities.Enum;
using Hotel.Services;
using Hotel.Services.DTOs.Offer;
using Hotel.Services.Interfaces;
using HotelSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : ControllerBase
    {
        private readonly OfferService _offerService;

        public OfferController(OfferService offerService)
        {
            _offerService = offerService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOfferById(int id)
        {
            var result = await _offerService.GetOfferByIdAsync(id);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }
        [HttpPost("add-offer")]
        public async Task<IActionResult> AddOffer([FromBody] AddOfferDTO dto)
        {
            var result = await _offerService.AddOfferAsync(dto);
            return HandleResponse(result);
        }


        [HttpPut("update-offer")]
        public async Task<IActionResult> UpdateOffer([FromBody] UpdateOfferDTO dto)
        {
            if (dto == null)
                return BadRequest("Invalid offer data.");

            var result = await _offerService.Update(dto);

            if (!result.IsSuccess)
                return NotFound(result); 

            return Ok(result);
        }


        private IActionResult HandleResponse<T>(ResponseViewModel<T> response)
        {
            if (response is ErrorResponseViewModel<T> error)
            {
                return StatusCode(400, new
                {
                    message = error.Message,
                    errorCode = error.ErrorCode.ToString()
                });
            }

            return Ok(new
            {
                data = response.Data,
                message = response.Message
            });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _offerService.Delete(id);
            return HandleResponse(result);
        }

    }
}
