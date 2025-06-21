using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Room
{
    public record RoomResponseDto(string RoomNumber, int Capacity, string? ImageUrl, decimal PricePerNight);
    
}
