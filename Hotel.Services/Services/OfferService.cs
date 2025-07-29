using AutoMapper;
using Hotel.Core.Entities;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Interfaces;
using Hotel.Services.DTOs.Offer;
using Hotel.Services.Interfaces;
using HotelSystem.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Hotel.Services
{
    public class OfferService
    {
        private readonly IRepository<Room> _roomrepository;
        private readonly IRepository<Offer> _repository;
        private readonly IRepository<OfferRoom> _offerRoomRepository;
        IMapper _mapper;
        public OfferService(IRepository<Offer> repository, IMapper mapper, IRepository<Room> roomrepository, IRepository<OfferRoom> offerRoomRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _roomrepository = roomrepository;
            _offerRoomRepository = offerRoomRepository;
        }

        public async Task<ResponseViewModel<bool>> AddOfferAsync(AddOfferDTO dto)
        {
            if (dto == null)
                return new ErrorResponseViewModel<bool>("Offer data is required.", ErrorCode.ValidationError);

            var offer = _mapper.Map<Offer>(dto);
            offer.OfferRooms = new List<OfferRoom>();

            foreach (var roomDto in dto.Rooms)
            {
                var room = await _roomrepository.GetByIdAsync(roomDto.RoomId);
                if (room == null)
                    continue;

                offer.OfferRooms.Add(new OfferRoom
                {
                    RoomId = roomDto.RoomId,
                    Offer = offer
                });
            }

            try
            {
                await _repository.AddAsync(offer);
                return new SuccessResponseViewModel<bool>(true, "Offer added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                throw;
            }
        }

        public async Task<ResponseViewModel<bool>> Update(UpdateOfferDTO dto)
        {
            if (dto == null)
                return new ErrorResponseViewModel<bool>("Offer data is required.", ErrorCode.ValidationError);

            var offerEntity = _mapper.Map<Offer>(dto);

            var updated = await _repository.UpdatePartialAsync(offerEntity,
                nameof(Offer.Name),
                nameof(Offer.Description),
                nameof(Offer.DiscountPercentage),
                nameof(Offer.StartDate),
                nameof(Offer.EndDate));

            if (!updated)
                return new ErrorResponseViewModel<bool>("Offer not found or not updated", ErrorCode.NotFound);

            return new SuccessResponseViewModel<bool>(true, "Offer updated successfully.");
        }


        public async Task<ResponseViewModel<OfferDetailsDTO>> GetOfferByIdAsync(int offerId)
        {
            if (offerId <= 0)
                return new ErrorResponseViewModel<OfferDetailsDTO>("Invalid ID", ErrorCode.ValidationError);

            var offer = await _repository.GetByIdAsync(offerId);
            if (offer == null || offer.IsDeleted)
                return new ErrorResponseViewModel<OfferDetailsDTO>("Offer not found", ErrorCode.NotFound);

            var offerRooms = await _offerRoomRepository.GetAll()
                .Where(or => or.OfferId == offerId && !or.IsDeleted)
                .ToListAsync();

            var allRooms = await _roomrepository.GetAll()
                .Where(r => offerRooms.Select(or => or.RoomId).Contains(r.Id))
                .ToListAsync();

            foreach (var or in offerRooms)
            {
                or.Offer = offer;
                or.Room = allRooms.FirstOrDefault(r => r.Id == or.RoomId);
            }

            offer.OfferRooms = offerRooms;

            var dto = _mapper.Map<OfferDetailsDTO>(offer);

            foreach (var room in dto.Rooms)
            {
                room.PriceAfterDiscount = room.OriginalPrice - (room.OriginalPrice * dto.DiscountPercentage / 100);
            }

            return new SuccessResponseViewModel<OfferDetailsDTO>(dto, "Offer retrieved successfully");
        }


        public async Task<ResponseViewModel<bool>> Delete(int id)
        {
            if (id <= 0)
            {
                return new ErrorResponseViewModel<bool>("Invalid Room ID", ErrorCode.ValidationError);
            }


            var entity = await _repository.GetByIdAsyncWithTracking(id);
            if (entity == null)
            {
                return new ErrorResponseViewModel<bool>("Room Not Found", ErrorCode.NotFound);
            }


            await _repository.DeleteAsync(entity);

            return new SuccessResponseViewModel<bool>(true, "Room deleted successfully");


        }



    }
}