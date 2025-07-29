using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
    public class Ads : BaseEntity
    {
        public int RoomId { get; set; }
        public string? Title { get; set; }
        public decimal Discount { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Room Room { get; set; }
    }
}
