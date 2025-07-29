using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
    public class Review : BaseEntity
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }
        public int ReservationId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        public User User { get; set; }
        public Room Room { get; set; }
        public Reservation Reservation { get; set; }
    }
}
