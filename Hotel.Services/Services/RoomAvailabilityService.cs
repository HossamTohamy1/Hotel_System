using Hotel.Core.Entities.Enum;
using Hotel.Core.Entities;
using Hotel.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Services.Services
{
    public class RoomAvailabilityService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public RoomAvailabilityService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();

                var reservationRepo = scope.ServiceProvider.GetRequiredService<IRepository<Reservation>>();
                var roomRepo = scope.ServiceProvider.GetRequiredService<IRepository<Room>>();
                var resRoomRepo = scope.ServiceProvider.GetRequiredService<IRepository<ReservationRoom>>();

                var reservations = reservationRepo
                           .Get(r => r.CheckOutDate <= DateTime.UtcNow && r.Status == ReservationStatus.Pending)
                           .ToList();

                foreach (var reservation in reservations)
                {
                    var resRooms = resRoomRepo
                        .Get(rr => rr.ReservationId == reservation.Id)
                        .ToList();

                    foreach (var resRoom in resRooms)
                    {
                        var room = await roomRepo.GetByIdAsync(resRoom.RoomId);
                        if (room != null && !room.IsAvailable)
                        {
                            room.IsAvailable = true;
                            await roomRepo.UpdateAsync(room);
                        }
                    }

                    reservation.Status = ReservationStatus.Completed;
                    await reservationRepo.UpdateAsync(reservation);
                }

                await Task.Delay(TimeSpan.FromDays(2), stoppingToken);
            }
        }
    }
}
