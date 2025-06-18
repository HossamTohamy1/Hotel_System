using Hotel.Core.Entities;
using Hotel.Services.DTOs;
using Hotel.Services.DTOs.Room; 

namespace Hotel.Services.Interfaces
{
    public interface IRoomServices
    {
        IQueryable<Room> GetAll();
        Task<Room> GetByIdAsync(int id);
        IEnumerable<Room> GetAvailableRoomsAsync();
        Task AddAsync(AddRoomDTO dto);
        Task<bool> Update(UpdateRoomDTO dto);
        Task<bool> Delete(int id);
        Task SaveAsync();
    }
}
