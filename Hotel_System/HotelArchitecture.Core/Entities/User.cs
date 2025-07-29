using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Entities
{
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string Role { get; set; } = "Guest";


        public ICollection<Reservation> Reservations { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public ICollection<UserFavoriteRoom> FavoriteRooms { get; set; }
    }
}
