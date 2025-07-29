using Hotel.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using Hotel.Infrastructure;
using Hotel.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.VisualBasic;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Text.Json.Serialization;

namespace HotelSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddInfrastructure(connectionString);
            builder.Services.AddApplicationServices();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<Program>();

            // JWT Authentication setup
            var key = Encoding.ASCII.GetBytes(Hotel.Infrastructure.Presistance.Data.Constants.SecretKey); 
            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("All", policy =>
                    policy.RequireRole("Admin", "Receptionist", "Guest"));

                options.AddPolicy("Admin", policy =>
                    policy.RequireRole("Admin"));

                options.AddPolicy("Receptionist", policy =>
                    policy.RequireRole("Receptionist"));

                options.AddPolicy("Guest", policy =>
                    policy.RequireRole("Guest"));
            });



            var app = builder.Build();

            // Middleware
            app.UseAuthentication();
            app.UseAuthorization();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/swagger");
                    return;
                }
                await next();
            });

            app.UseHttpsRedirection();
            app.MapControllers();

            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<HotelDbContext>();

                context.Database.Migrate(); // Apply migrations

                await HotelDbContextSeed.SeedAdminAsync(context);
            }

            app.Run();
        }
    }
}
