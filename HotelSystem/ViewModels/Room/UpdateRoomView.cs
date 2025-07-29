using Hotel.Core.Entities.Enum;

namespace HotelSystem.ViewModels.Room
{
    public class UpdateRoomView
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }
    }
}
