using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Offer
{
    public class RoomWithDiscountDTO
    {
        public int RoomId { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; }
    }
}
