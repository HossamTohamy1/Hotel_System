using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Reservation
{
    public class ReservationRoomDTO
    {
        public int RoomId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
