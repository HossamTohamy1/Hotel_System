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
<<<<<<< HEAD
        Task AddAsync(AddRoomDTO dto);
=======
        Task <ResponseViewModel<bool>> AddAsync(AddRoomDTO dto);
>>>>>>> d6d277f (add new feature)
        Task<ResponseViewModel<bool>> Update(UpdateRoomDTO dto);
        Task<ResponseViewModel<bool>> Delete(int id);
        Task SaveAsync();

    }
}
