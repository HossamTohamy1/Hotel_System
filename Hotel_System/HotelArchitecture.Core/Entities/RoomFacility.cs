﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
    public class RoomFacility : BaseEntity
    {
        public int RoomId { get; set; }
        public int FacilityId { get; set; }

        public Room Room { get; set; }
        public Facility Facility { get; set; }
    }
}
