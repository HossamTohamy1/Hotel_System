using Hotel.Services.Interfaces;
using Hotel.Infrastructure.Presistance.Data;
using Hotel.Infrastructure.Presistance;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Hotel.Services;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Hotel.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<HotelDbContext>(options =>
                options.UseSqlServer(connectionString)
                       .LogTo(msg => Debug.WriteLine(msg), LogLevel.Information)
                       .EnableSensitiveDataLogging(true)
                       .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IRoomServices, RoomServices>();


            return services;
        }
    }
}
