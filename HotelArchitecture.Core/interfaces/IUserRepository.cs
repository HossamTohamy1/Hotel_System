using Hotel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Core.Interfaces
{
    public interface IUserRepository
    {
        Task<Customer> GetCustomerByIdAsync(int id);
        Task<Staff> PromoteCustomerToStaffAsync(Customer customer, Staff staff);
    }
}
