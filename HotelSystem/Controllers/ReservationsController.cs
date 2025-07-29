using Hotel.Services.DTOs.Reservation;
using Hotel.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationsController(ReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationDTO dto)
        {
            try
            {
                var result = await _reservationService.CreateReservationAsync(dto);
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); 
            }
        }
    }
}
