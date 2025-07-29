using Hotel.Core.Entities;
using Hotel.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.DTOs;
using Hotel.Services.DTOs.Room;
using Hotel.Core.Entities.Enum;
using HotelSystem.ViewModels;
using Hotel.Services;
<<<<<<< HEAD
=======
using Microsoft.AspNetCore.Authorization;
using AutoMapper.Features;
using Microsoft.Extensions.Logging;
>>>>>>> d6d277f (add new feature)

namespace HotelSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomServices _roomServices;
        private readonly ILogger<RoomController> _logger;

        public RoomController(IRoomServices roomServices, ILogger<RoomController> logger)
        {
            _roomServices = roomServices;
            _logger = logger;
        }
<<<<<<< HEAD
        [HttpGet("GET ROOM")]
        
        public ResponseViewModel<IEnumerable<RoomResponseDto>> GetAllRooms()
=======

        [Authorize]
        [TypeFilter(typeof(CustomAuthorizeFilter), Arguments = new object[] { "GetAllRooms" })]
        [HttpGet("get-all-room")]
        public IActionResult GetAllRooms()
>>>>>>> d6d277f (add new feature)
        {
            _logger.LogInformation("GetAllRooms endpoint called");
            var result = _roomServices.GetAll();
<<<<<<< HEAD
            return result;
        }

        [HttpGet("GET BY ID")]
        public async Task<ResponseViewModel<RoomResponseDto>> GetById(int id)
        {
            var result = await _roomServices.GetByIdAsync(id);
            return result;
        }

        [HttpGet("GET ONLY AVALIABLE ROOM")]

        public ResponseViewModel<IEnumerable<RoomResponseDto>> GetRooms()
        {
            var result = _roomServices.GetAvailableRoomsAsync();
            return result;
=======
            _logger.LogInformation("GetAllRooms endpoint completed");
            return HandleResponse(result);
        }

        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById(int id)
        {
            _logger.LogInformation("GetById endpoint called with id: {Id}", id);
            var result = await _roomServices.GetByIdAsync(id);
            _logger.LogInformation("GetById endpoint completed for id: {Id}", id);
            return HandleResponse(result);
        }

        [HttpGet("get-only-avaliable-room")]
        public IActionResult GetRooms()
        {
            _logger.LogInformation("GetRooms endpoint called");
            var result = _roomServices.GetAvailableRoomsAsync();
            _logger.LogInformation("GetRooms endpoint completed");
            return HandleResponse(result);
>>>>>>> d6d277f (add new feature)
        }
        [Authorize]
        [TypeFilter(typeof(CustomAuthorizeFilter), Arguments = new object[] { "AddRoom" })]
        [HttpPost]
<<<<<<< HEAD
        public async Task<ResponseViewModel<bool>> AddRoom([FromBody]AddRoomDTO dto)
        {
            await _roomServices.AddAsync(dto);
            return new SuccessResponseViewModel<bool>(true, "Room added successfully");
        }
        [HttpPut]
        public async Task<ResponseViewModel<bool>> Update(UpdateRoomDTO dto)
        {
            var updated = await _roomServices.Update(dto);

            return updated;
=======
        public async Task<IActionResult> AddRoom([FromBody] AddRoomDTO dto)
        {
            _logger.LogInformation("AddRoom endpoint called");
            var result = await _roomServices.AddAsync(dto);
            _logger.LogInformation("AddRoom endpoint completed");
            return HandleResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoomDTO dto)
        {
            _logger.LogInformation("Update endpoint called");
            var result = await _roomServices.Update(dto);
            _logger.LogInformation("Update endpoint completed");
            return HandleResponse(result);
>>>>>>> d6d277f (add new feature)
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
<<<<<<< HEAD
            var result = await _roomServices.Delete(id);
            return result;
=======
            _logger.LogInformation("Delete endpoint called with id: {Id}", id);
            var result = await _roomServices.Delete(id);
            _logger.LogInformation("Delete endpoint completed for id: {Id}", id);
            return HandleResponse(result);
>>>>>>> d6d277f (add new feature)
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


    }
}
