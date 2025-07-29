using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Auth
{
    public class AddCustomerDTO
    {
        public string Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? LoyaltyNumber { get; set; }
        public DateTime? LoyaltySince { get; set; }
    }
}
