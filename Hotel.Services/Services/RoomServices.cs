using AutoMapper;
using Hotel.Core.Entities;
using Hotel.Services.Interfaces;
using Hotel.Services.DTOs;
using Hotel.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hotel.Services.DTOs.Room;
using HotelSystem.ViewModels;
using Hotel.Core.Entities.Enum;

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

        public ResponseViewModel<IEnumerable<RoomResponseDto>> GetAll()
        {
            var rooms = _repository.GetAll().Select(x=>new RoomResponseDto(x.RoomNumber,x.Capacity,x.ImageUrl,x.PricePerNight,x.IsAvailable));

            return new SuccessResponseViewModel<IEnumerable<RoomResponseDto>>(rooms, "The Data Returned Succeffully.");
        }

        public ResponseViewModel<IEnumerable<RoomResponseDto>> GetAvailableRoomsAsync()
        {
            var rooms= _repository.GetAll().Where(x => x.IsAvailable == true).Select(x => new RoomResponseDto(x.RoomNumber, x.Capacity, x.ImageUrl, x.PricePerNight,x.IsAvailable)); ;

            return new SuccessResponseViewModel<IEnumerable<RoomResponseDto>>(rooms, "Available rooms retrieved successfully");

        }

        public async Task<Room> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        async Task<ResponseViewModel<RoomResponseDto>> IRoomServices.GetByIdAsync(int id)
        {
            if (id <= 0)
                return new ErrorResponseViewModel<RoomResponseDto>("Invalid ID", ErrorCode.ValidationError);
            var room = await _repository.GetByIdAsync(id);
            if(room is null)
                return new ErrorResponseViewModel<RoomResponseDto>("Room not found", ErrorCode.ValidationError);

           var response= _mapper.Map<RoomResponseDto>(room);

            return new SuccessResponseViewModel<RoomResponseDto>(response, "Room retrieved successfully");
        }

        public async Task<ResponseViewModel<bool>> AddAsync(AddRoomDTO dto)
        {
            if (dto == null)
            {
                return new ErrorResponseViewModel<bool>("Room data is required.", ErrorCode.ValidationError);
            }

            var entity = _mapper.Map<Room>(dto);

            try
            {
                await _repository.AddAsync(entity);
                return new SuccessResponseViewModel<bool>(true, "Room added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message);
                throw;
            }
        }
        public async Task<ResponseViewModel<bool>> Update(UpdateRoomDTO dto)
        {
            var query = _mapper.Map<Room>(dto);
            var update= await _repository.UpdatePartialAsync(query,
                nameof(Room.RoomNumber),
                nameof(Room.Type),
                nameof(Room.Capacity),
                nameof(Room.PricePerNight),
                nameof(Room.Description));
            if(!update)
                return new ErrorResponseViewModel<bool>("Room not found or not updated", ErrorCode.NotFound);


            return new SuccessResponseViewModel<bool>(true, "Room updated successfully");
        }

        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResponseViewModel<bool>("Invalid Room ID", ErrorCode.ValidationError);
            }
            

                var entity = await _repository.GetByIdAsyncWithTracking(id);
                if (entity == null)
                {
                    return new ErrorResponseViewModel<bool>("Room Not Found", ErrorCode.NotFound);
                }


                await _repository.DeleteAsync(entity);

                return new SuccessResponseViewModel<bool>(true, "Room deleted successfully");
            
            
        }

        public async Task SaveAsync()
        {
            await _repository.SaveAsync();
        }

      

       
    }
}
