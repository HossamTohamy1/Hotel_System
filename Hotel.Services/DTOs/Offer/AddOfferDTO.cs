using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Offer
{
    public class AddOfferDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal DiscountPercentage { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<OfferRoomDTO> Rooms { get; set; }
    }
}
