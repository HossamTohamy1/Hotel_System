using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Room
{
<<<<<<< HEAD
    public record RoomResponseDto(string RoomNumber, int Capacity, string? ImageUrl, decimal PricePerNight);
=======
    public record RoomResponseDto(string RoomNumber, int Capacity, string? ImageUrl, decimal PricePerNight , bool IsAvailable);
>>>>>>> d6d277f (add new feature)
    
}
