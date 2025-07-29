using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
    public class Facility : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconClass { get; set; }

        public ICollection<RoomFacility> RoomFacilities { get; set; }
    }
}
