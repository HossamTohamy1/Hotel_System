using AutoMapper;
using Hotel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Auth
{
    class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<RegistrationDTO, Customer>()
      .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.CustomerInfo.Address))
      .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.CustomerInfo.DateOfBirth))
      .ForMember(dest => dest.LoyaltyNumber, opt => opt.MapFrom(src => src.CustomerInfo.LoyaltyNumber))
      .ForMember(dest => dest.LoyaltySince, opt => opt.MapFrom(src => src.CustomerInfo.LoyaltySince))
      .ForMember(dest => dest.Role, opt => opt.Ignore()); // لازم عشان متحاولش يعمل Map لكائن Role


            CreateMap<LoginDTO, User>();
            CreateMap<AddCustomerDTO, Customer>();

        }
    }
}

