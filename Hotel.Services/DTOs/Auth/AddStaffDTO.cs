using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Auth
{
    public class AddStaffDTO
    {
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }
        public decimal Salary { get; set; }
    }
}
