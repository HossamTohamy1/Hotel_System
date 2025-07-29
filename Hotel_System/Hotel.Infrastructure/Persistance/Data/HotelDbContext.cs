using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Hotel.Core.Entities;
using Hotel.Core.Entities.Enum;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel.Infrastructure.Presistance.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<ReservationRoom> ReservationRooms { get; set; }
        public DbSet<UserFavoriteRoom> UserFavoriteRooms { get; set; }
        public DbSet<Ads> ads { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var method = typeof(HotelDbContext)
                        .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Instance)
                        .MakeGenericMethod(entityType.ClrType);
                    method.Invoke(this, new object[] { modelBuilder });
                }
            }

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(u => u.Role).HasDefaultValue("Guest");

            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasIndex(r => r.RoomNumber).IsUnique();

                entity.Property(r => r.IsAvailable)
                      .HasDefaultValue(true);

                entity.Property(r => r.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(r => r.ImageUrl)
                      .HasMaxLength(300); 

                entity.Property(r => r.Tag)
                      .HasMaxLength(100);

                entity.Property(r => r.Discount)
                      .HasPrecision(5, 2); 
            });


            modelBuilder.Entity<RoomFacility>(entity =>
            {
                entity.HasKey(rf => new { rf.RoomId, rf.FacilityId });
                entity.HasOne(rf => rf.Room).WithMany(r => r.RoomFacilities).HasForeignKey(rf => rf.RoomId);
                entity.HasOne(rf => rf.Facility).WithMany(f => f.RoomFacilities).HasForeignKey(rf => rf.FacilityId);
                entity.Property(rf => rf.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            

            modelBuilder.Entity<ReservationRoom>(entity =>
            {
                entity.HasKey(rr => new { rr.ReservationId, rr.RoomId });
                entity.HasOne(rr => rr.Reservation).WithMany(r => r.ReservationRooms).HasForeignKey(rr => rr.ReservationId);
                entity.HasOne(rr => rr.Room).WithMany(r => r.ReservationRooms).HasForeignKey(rr => rr.RoomId);
                entity.Property(rr => rr.Quantity).HasDefaultValue(1);
                entity.Property(rr => rr.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<UserFavoriteRoom>(entity =>
            {
                entity.HasKey(ufr => new { ufr.UserId, ufr.RoomId });
                entity.HasOne(ufr => ufr.User).WithMany(u => u.FavoriteRooms).HasForeignKey(ufr => ufr.UserId);
                entity.HasOne(ufr => ufr.Room).WithMany(r => r.UserFavorites).HasForeignKey(ufr => ufr.RoomId);
                entity.Property(ufr => ufr.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(r => r.Reservation).WithMany().HasForeignKey(r => r.ReservationId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(r => r.Room).WithMany(rm => rm.Reviews).HasForeignKey(r => r.RoomId).OnDelete(DeleteBehavior.Restrict);
                entity.Property(r => r.Rating).HasAnnotation("Range", new[] { 1, 5 });
                entity.Property(r => r.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasOne(p => p.Reservation).WithOne(r => r.Payment).HasForeignKey<Payment>(p => p.ReservationId);
                entity.Property(p => p.PaymentDate).HasDefaultValueSql("GETUTCDATE()");
                entity.Property(p => p.Status).HasDefaultValue(PaymentStatus.Pending);
                entity.Property(p => p.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(r => r.Status).HasDefaultValue(ReservationStatus.Pending);
                entity.Property(r => r.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });


            modelBuilder.Entity<Ads>()
                .HasOne(ad => ad.Room)
                .WithMany() 
                .HasForeignKey(ad => ad.RoomId)
                .OnDelete(DeleteBehavior.Cascade);

            
            modelBuilder.Entity<Ads>()
                .Property(a => a.Discount)
                .HasPrecision(5, 2);
        }

        public override int SaveChanges()
        {
            UpdateSoftDeleteStatuses();
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateSoftDeleteStatuses();
            UpdateTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateSoftDeleteStatuses()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }

        private void UpdateTimestamps()
        {
            var entries = ChangeTracker.Entries<BaseEntity>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }
                else
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }
        }

        private void SetSoftDeleteFilter<TEntity>(ModelBuilder builder) where TEntity : BaseEntity
        {
            builder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }
    }

    public class HotelDbContextSeed
    {
       public static async Task SeedAdminAsync(HotelDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User
                {
                    Username = "admin",
                    Email ="Admin@Gmail.com",
                    PasswordHash = "admin123",
                    Phone = "01022554525",
                    Role = "Admin"
                });

                await context.SaveChangesAsync();
            }
        }
    }
}
