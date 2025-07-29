using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Reservation
{
    public class ReservationRoomDetailsDTO
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public decimal PriceAtReservation { get; set; }
        public int Quantity { get; set; }
    }
}
