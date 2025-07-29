using AutoMapper;
using Hotel.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.DTOs.Offer
{
    public class OfferProfile :Profile
    {
        public OfferProfile() 
        {
        CreateMap<AddOfferDTO, Core.Entities.Offer>();
        CreateMap<UpdateOfferDTO, Core.Entities.Offer>();

            CreateMap<Core.Entities.Offer, OfferDetailsDTO>()
    .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.OfferRooms));

            CreateMap<OfferRoom, RoomWithDiscountDTO>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.Room.Id))
                .ForMember(dest => dest.OriginalPrice, opt => opt.MapFrom(src => src.Room.PricePerNight))
                .ForMember(dest => dest.PriceAfterDiscount, opt => opt.MapFrom(src =>
                    src.Room.PricePerNight - (src.Room.PricePerNight * src.Offer.DiscountPercentage / 100)));

        }
    }
}
