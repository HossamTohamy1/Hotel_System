using Hotel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Feature_Role
{
    public class CheckFeatureAccessDTO
    {
        public Role Role { get; set; }
        public Feature Feature { get; set; }
    }
}

