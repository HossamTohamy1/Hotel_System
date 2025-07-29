using System;
using System.Collections.Generic;

namespace Hotel.Core.Entities
{
    public class Customer : User
    {
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string LoyaltyNumber { get; set; }
        public DateTime? LoyaltySince { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserFavoriteRoom> FavoriteRooms { get; set; }
    }

}
