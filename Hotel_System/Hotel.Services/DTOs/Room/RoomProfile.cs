using AutoMapper;
using Hotel.Services.DTOs.Room;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Hotel.Services.DTOs
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<AddRoomDTO, Hotel.Core.Entities.Room>();
            CreateMap<UpdateRoomDTO, Hotel.Core.Entities.Room>();


        }
    }
}
