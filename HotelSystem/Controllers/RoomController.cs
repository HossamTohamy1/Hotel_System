using Hotel.Core.Entities;
using Hotel.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.DTOs;
using Hotel.Services.DTOs.Room;
using Hotel.Core.Entities.Enum;
using HotelSystem.ViewModels;

namespace HotelSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomServices _roomServices;

        public RoomController(IRoomServices roomServices)
        {
            _roomServices = roomServices;
        }
        [HttpGet("GET ROOM")]
        
        public ResponseViewModel<IEnumerable<Room>> GetAllRooms()
        {
            var result = _roomServices.GetAll();
            return new SuccessResponseViewModel<IEnumerable<Room>>(result, "Rooms retrieved successfully");
        }

        [HttpGet("GET BY ID")]
        public async Task<ResponseViewModel<Room>> GetById(int id)
        {
            if (id <= 0)
                return new ErrorResponseViewModel<Room>("Invalid ID", ErrorCode.ValidationError);

            var room = await _roomServices.GetByIdAsync(id);

            if (room == null)
                return new ErrorResponseViewModel<Room>("Room not found", ErrorCode.NotFound);

            return new SuccessResponseViewModel<Room>(room, "Room retrieved successfully");
        }

        [HttpGet("GET ONLY AVALIABLE ROOM")]

        public ResponseViewModel<IEnumerable<Room>> GetRooms()
        {
            var availableRooms = _roomServices.GetAvailableRoomsAsync();
            return new SuccessResponseViewModel<IEnumerable<Room>>(availableRooms, "Available rooms retrieved successfully");
        }
        [HttpPost]
        public async Task<ResponseViewModel<bool>> AddRoom([FromBody] AddRoomDTO dto)
        {
            if (!ModelState.IsValid)
                return new ErrorResponseViewModel<bool>("Invalid input data", ErrorCode.ValidationError);

            await _roomServices.AddAsync(dto);
            return new SuccessResponseViewModel<bool>(true, "Room added successfully");
        }
        [HttpPut]
        public async Task<ResponseViewModel<bool>> Update(UpdateRoomDTO dto)
        {
            var updated = await _roomServices.Update(dto);
            if (!updated)
                return new ErrorResponseViewModel<bool>("Room not found or not updated", ErrorCode.NotFound);

            return new SuccessResponseViewModel<bool>(true, "Room updated successfully");
        }

        [HttpDelete]
        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResponseViewModel<bool>("Invalid course ID", ErrorCode.ValidationError);
            }

            var deleted = await _roomServices.Delete(id);
            if (!deleted)
            {
                return new ErrorResponseViewModel<bool>("Course not found", ErrorCode.NotFound);
            }

            return new SuccessResponseViewModel<bool>(true, "Course deleted successfully");
        }
    }
}
