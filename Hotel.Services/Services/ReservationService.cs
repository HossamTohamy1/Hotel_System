using AutoMapper;
using Hotel.Core.Entities;
using Hotel.Core.Entities.Enum;
using Hotel.Core.Interfaces;
using Hotel.Services.DTOs.Reservation;
using HotelSystem.Exceptions;
using Microsoft.Extensions.Logging;

namespace Hotel.Services.Services
{
    public class ReservationService
    {
        private readonly IRepository<Reservation> _reservationRepo;
        private readonly IRepository<Room> _roomRepo;
        private readonly IRepository<ReservationRoom> _resRoomRepo;
        private readonly IRepository<User> _userRepo;
        private readonly ILogger<ReservationService> _logger;
        private readonly IMapper _mapper;

        public ReservationService(
            IRepository<Reservation> reservationRepo,
            IRepository<Room> roomRepo,
            IRepository<ReservationRoom> resRoomRepo,
            IRepository<User> userRepo,
            ILogger<ReservationService> logger,
            IMapper mapper)
        {
            _reservationRepo = reservationRepo;
            _roomRepo = roomRepo;
            _resRoomRepo = resRoomRepo;
            _userRepo = userRepo;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ReservationResponseDTO> CreateReservationAsync(CreateReservationDTO dto)
        {
            _logger.LogInformation("Starting reservation creation for user {UserId}", dto.UserId);

            var user = await _userRepo.GetByIdAsync(dto.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found.", ErrorCode.NotFound);
            }

            var nights = (dto.CheckOutDate - dto.CheckInDate).Days;
            if (nights <= 0)
            {
                throw new ValidationException("Invalid check-in/check-out dates.");
            }

            decimal totalPrice = 0;
            var resRooms = new List<ReservationRoom>();

            foreach (var roomDto in dto.Rooms)
            {
                var room = await _roomRepo.GetByIdAsync(roomDto.RoomId);
                if (room == null || !room.IsAvailable)
                {
                    throw new ValidationException($"Room {roomDto.RoomId} is not available.");
                }

                var pricePerNightAfterDiscount = room.PricePerNight * (1 - room.Discount);
                var totalRoomPrice = pricePerNightAfterDiscount * nights * roomDto.Quantity;

                totalPrice += totalRoomPrice;

                room.IsAvailable = false;
                await _roomRepo.UpdateAsync(room);

                resRooms.Add(new ReservationRoom
                {
                    RoomId = roomDto.RoomId,
                    Quantity = roomDto.Quantity,
                    PriceAtReservation = totalRoomPrice
                });

                _logger.LogInformation("Room {RoomId} reserved: Quantity = {Qty}, Price = {Price}",
                    roomDto.RoomId, roomDto.Quantity, totalRoomPrice);
            }

            var reservation = new Reservation
            {
                UserId = dto.UserId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                TotalPrice = totalPrice,
                Status = ReservationStatus.Pending,
                ReservationRooms = resRooms
            };

            await _reservationRepo.AddAsync(reservation);

            _logger.LogInformation("Reservation created successfully for user {UserId} with total price {Price}", dto.UserId, totalPrice);

            return _mapper.Map<ReservationResponseDTO>(reservation);
        }
    }
}
