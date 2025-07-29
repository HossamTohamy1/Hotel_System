using Hotel.Infrastructure.Presistance.Data;
using Microsoft.EntityFrameworkCore;
using Hotel.Infrastructure;
using Hotel.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
<<<<<<< HEAD
using Microsoft.VisualBasic;
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Text.Json.Serialization;
=======
using FluentValidation.AspNetCore;
using FluentValidation;
using System.Text.Json.Serialization;
using HotelSystem.Middlewares;
using Hotel.Services.Services;
using Serilog;
>>>>>>> d6d277f (add new feature)

namespace HotelSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .CreateLogger();

<<<<<<< HEAD
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
=======
            try
>>>>>>> d6d277f (add new feature)
            {
                Log.Information("Starting Hotel System API");

                var builder = WebApplication.CreateBuilder(args);

                builder.Host.UseSerilog();

                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

                builder.Services.AddInfrastructure(connectionString);
                builder.Services.AddApplicationServices();
                builder.Services.AddLocalization(options => options.ResourcesPath = "Resources"); 

                builder.Services.AddScoped<RoleFeatureService>();
                builder.Services.AddScoped<OfferService>();
                builder.Services.AddScoped<UserService>();
                builder.Services.AddScoped<FeatureServices>();
                builder.Services.AddScoped<RoleServices>();
                builder.Services.AddScoped<ReservationService>();
                builder.Services.AddHostedService<RoomAvailabilityService>();
                builder.Services.AddTransient<GlobalErrorHandlerMiddleware>();

                builder.Services.AddControllers().AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
                builder.Services.AddFluentValidationAutoValidation();
                builder.Services.AddValidatorsFromAssemblyContaining<Program>();

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

                var app = builder.Build();
                app.UseMiddleware<GlobalErrorHandlerMiddleware>();
                app.UseMiddleware<TransactionMiddleware>();

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
                    context.Database.Migrate();
                    await HotelDbContextSeed.SeedAdminAsync(context);
                }

                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Application failed to start.");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
