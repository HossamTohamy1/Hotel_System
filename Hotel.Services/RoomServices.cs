using AutoMapper;
using Hotel.Core.Entities;
using Hotel.Services.Interfaces;
using Hotel.Services.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Services.DTOs.Room;

namespace Hotel.Services
{
    public class RoomServices : IRoomServices
    {
        private readonly IRepository<Room> _repository;
        IMapper _mapper;
        public RoomServices(IRepository<Room> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public IQueryable<Room> GetAll()
        {
            return _repository.GetAll();
        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(AddRoomDTO dto)
        {
            var entity = _mapper.Map<Room>(dto);
            await _repository.AddAsync(entity);
        }
        public async Task<bool> Update(UpdateRoomDTO dto)
        {
            var query = _mapper.Map<Room>(dto);
            return await _repository.UpdatePartialAsync(query,
                nameof(Room.RoomNumber),
                nameof(Room.Type),
                nameof(Room.Capacity),
                nameof(Room.PricePerNight),
                nameof(Room.Description));
        }

        public async Task<bool> Delete(int id)
        {
            try
            {

                var entity = await _repository.GetByIdAsync(id);
                if (entity == null)
                {
                    return false;
                }


                await _repository.DeleteAsync(id);

                return true;
            }
            catch (Exception ex)
            {


                throw;
            }
        }

        public async Task SaveAsync()
        {
            await _repository.SaveAsync();
        }

        public IEnumerable<Room> GetAvailableRoomsAsync()
        {
            return _repository.GetAll().Where(x=>x.IsAvailable==true);
        }

        
    }
}
