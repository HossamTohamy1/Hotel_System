﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
    public class UserFavoriteRoom : BaseEntity
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public User User { get; set; }
        public Room Room { get; set; }
    }
}
