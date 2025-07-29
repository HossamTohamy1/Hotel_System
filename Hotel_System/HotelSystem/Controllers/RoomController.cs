using Hotel.Core.Entities;
using Hotel.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Hotel.Services.DTOs;
using Hotel.Services.DTOs.Room;
using Hotel.Core.Entities.Enum;
using HotelSystem.ViewModels;
using Hotel.Services;

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
        
        public ResponseViewModel<IEnumerable<RoomResponseDto>> GetAllRooms()
        {
            var result = _roomServices.GetAll();
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
        }
        [HttpPost]
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
        }

        [HttpDelete]
        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            var result = await _roomServices.Delete(id);
            return result;
        }
    }
}
