using Hotel.Core.Entities;
using Hotel.Core.Entities.Enum;
using Hotel.Services;
using Hotel.Services.DTOs.Auth;
using Hotel.Services.DTOs.Room;
using Hotel.Services.Interfaces;
using Hotel.Core.Interfaces;
using HotelSystem.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Admin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IRoomServices _roomServices;
        private readonly IRepository<User> _userRepository;
        private readonly ILogger<AdminController> _logger;

        public AdminController(IRoomServices roomServices, IRepository<User> userRepository , ILogger<AdminController> logger)
        {
            _roomServices = roomServices;
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet("available-rooms")]
        public ResponseViewModel<IEnumerable<RoomResponseDto>> GetAvailableRooms()
        {
            //_logger.LogInformation("Fetching available rooms.");
            throw new Exception("An error occurred while fetching available rooms.");

            var result = _roomServices.GetAvailableRoomsAsync();
            return result;
        }

        [HttpGet("not-available-rooms")]
        public IActionResult GetNotAvailableRooms()
        {
            var result = _roomServices.GetAll();
            var notAvailableRooms = result.Data?.Where(r => !r.IsAvailable).ToList();

            if (notAvailableRooms == null || !notAvailableRooms.Any())
                return NotFound(new ErrorResponseViewModel<IEnumerable<RoomResponseDto>>(
                    "No not-available rooms found.",
                    ErrorCode.NotFound
                ));

            return Ok(new SuccessResponseViewModel<IEnumerable<RoomResponseDto>>(
                notAvailableRooms,
                "Not-available rooms retrieved successfully."
            ));
        }
     

    }
}

