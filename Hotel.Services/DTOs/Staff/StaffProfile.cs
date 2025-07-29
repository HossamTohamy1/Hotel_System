using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Staff
{
    public  class StaffProfile : Profile
    {
        public StaffProfile()
        {
            CreateMap<CustomerToStaffPromotionDto, Core.Entities.Staff>();

        }
    }
}
