using Hotel.Core.Entities.Enum;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Services.DTOs
{
    public class AddRoomDTO
    {
        public string RoomNum { get; set; }
        public RoomType Type { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }
    }
}
