using AutoMapper;
using Hotel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Reservation
{
    public class Reservationprofile : Profile
    {
        public Reservationprofile() 
        {
            CreateMap<Core.Entities.Reservation, ReservationResponseDTO>();
            CreateMap<CreateReservationDTO, Core.Entities.Reservation>();
        }
    }
}
