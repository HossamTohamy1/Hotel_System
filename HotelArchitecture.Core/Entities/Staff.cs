using System;

namespace Hotel.Core.Entities
{
    public class Staff : User
    {
        public string StaffNumber { get; set; }
        public string Position { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsManager { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserFavoriteRoom> FavoriteRooms { get; set; }
    }


}
