using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Reservation
{
    public class ReservationResponseDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; }
        public List<ReservationRoomDetailsDTO> Rooms { get; set; }
    }
}
