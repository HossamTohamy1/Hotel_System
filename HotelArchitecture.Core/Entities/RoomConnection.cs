using Hotel.Core.Entities.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
    public class RoomConnection : BaseEntity
    {
        public int RoomId1 { get; set; }
        public int RoomId2 { get; set; }
        public ConnectionType ConnectionType { get; set; }

        public Room Room1 { get; set; }
        public Room Room2 { get; set; }
    }
}
