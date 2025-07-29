using Hotel.Core.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
    public class Room : BaseEntity
    {
        public string RoomNumber { get; set; }
        public RoomType Type { get; set; }
        public int Capacity { get; set; }
        public decimal PricePerNight { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }        
        public decimal Discount { get; set; }        
        public string? Tag { get; set; }
        public bool IsAvailable { get; set; } = true;

        public ICollection<RoomFacility> RoomFacilities { get; set; }
        public ICollection<ReservationRoom> ReservationRooms { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserFavoriteRoom> UserFavorites { get; set; }
        public ICollection<Ads> Ads { get; set; }

    }
}
