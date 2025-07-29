using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
    public class OfferRoom : BaseEntity
    {
        public int OfferId { get; set; }
        public int RoomId { get; set; }
        public decimal? SpecialPrice { get; set; }

        public Offer Offer { get; set; }
        public Room Room { get; set; }
    }
}
