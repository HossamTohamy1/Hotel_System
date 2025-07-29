using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
    public class ReservationRoom : BaseEntity
    {
        public int ReservationId { get; set; }
        public int RoomId { get; set; }
        public int Quantity { get; set; } = 1;
        public decimal PriceAtReservation { get; set; }

        public Reservation Reservation { get; set; }
        public Room Room { get; set; }
    }
}
