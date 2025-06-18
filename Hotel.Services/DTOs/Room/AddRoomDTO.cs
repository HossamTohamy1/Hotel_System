using Hotel.Core.Entities.Enum;

namespace Hotel.Services.DTOs
{
    public class AddRoomDTO
    {
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }
    }
}
