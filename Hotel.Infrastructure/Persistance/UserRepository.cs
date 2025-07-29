using Hotel.Core.Entities;
using Hotel.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using Hotel.Core.Interfaces;

namespace Hotel.Infrastructure.Persistance
{
    public class UserRepository : IUserRepository
    {
        private readonly HotelDbContext _context;

        public UserRepository(HotelDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetCustomerByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }


        public async Task<Staff> PromoteCustomerToStaffAsync(Customer customer, Staff staff)
        {
            try
            {
                staff.Username = customer.Username;
                staff.Email = customer.Email;
                staff.PasswordHash = customer.PasswordHash;
                staff.Phone = customer.Phone;

                await _context.Staffs.AddAsync(staff);

                if (customer.Reservations != null)
                {
                    foreach (var reservation in customer.Reservations)
                    {
                        reservation.User = staff;
                    }
                }

                if (customer.Reviews != null)
                {
                    foreach (var review in customer.Reviews)
                    {
                        review.User = staff;
                    }
                }

                if (customer.FavoriteRooms != null)
                {
                    foreach (var favoriteRoom in customer.FavoriteRooms)
                    {
                        favoriteRoom.User = staff;
                    }
                }

                _context.Customers.Remove(customer);

                await _context.SaveChangesAsync();

                return staff;
            }
            catch
            {
                throw;
            }
        }
    }
}
