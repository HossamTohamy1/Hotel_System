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
            CreateMap<RegistrationDTO, User>()
                    .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "Guest"));

            CreateMap<LoginDTO, User>();
        }
    }
}

