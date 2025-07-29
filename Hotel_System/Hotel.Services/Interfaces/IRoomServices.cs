using Hotel.Core.Entities;
using Hotel.Services.DTOs;
using Hotel.Services.DTOs.Room;
using HotelSystem.ViewModels;

namespace Hotel.Services.Interfaces
{
    public interface IRoomServices
    {
        ResponseViewModel<IEnumerable<RoomResponseDto>> GetAll();
        Task<ResponseViewModel<RoomResponseDto>> GetByIdAsync(int id);
        ResponseViewModel<IEnumerable<RoomResponseDto>> GetAvailableRoomsAsync();
        Task AddAsync(AddRoomDTO dto);
        Task<ResponseViewModel<bool>> Update(UpdateRoomDTO dto);
        Task<ResponseViewModel<bool>> Delete(int id);
        Task SaveAsync();
    }
}
