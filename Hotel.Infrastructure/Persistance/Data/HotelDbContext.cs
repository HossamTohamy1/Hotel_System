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
using System.Collections;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Identity;

namespace Hotel.Infrastructure.Presistance.Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<ReservationRoom> ReservationRooms { get; set; }
        public DbSet<UserFavoriteRoom> UserFavoriteRooms { get; set; }
        public DbSet<Ads> ads { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<RoleFeature> RoleFeatures { get; set; }
        public DbSet<OfferRoom> OfferRooms { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<RoomConnection> RoomConnections { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var clrType = entityType.ClrType;

                if (typeof(BaseEntity).IsAssignableFrom(clrType) &&
                    entityType.BaseType == null)
                {
                    entityType.AddSoftDeleteQueryFilter();
                }
            }

            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<Customer>().ToTable("Customers");

            modelBuilder.Entity<Staff>().ToTable("Staff");



            modelBuilder.Entity<Role>().ToTable("Roles");
            modelBuilder.Entity<Feature>().ToTable("Features");
            modelBuilder.Entity<RoleFeature>().ToTable("RoleFeatures");
  
            
            // Role
            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany(r => r.RoleFeatures)
                      .WithOne(rf => rf.Role)
                      .HasForeignKey(rf => rf.RoleId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Feature
            modelBuilder.Entity<Feature>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.HasMany(f => f.RoleFeatures)
                      .WithOne(rf => rf.Feature)
                      .HasForeignKey(rf => rf.FeatureId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // RoleFeature
            modelBuilder.Entity<RoleFeature>(entity =>
            {
                entity.HasKey(rf => rf.Id);

                entity.HasOne(rf => rf.Role)
                      .WithMany(r => r.RoleFeatures)
                      .HasForeignKey(rf => rf.RoleId);

                entity.HasOne(rf => rf.Feature)
                      .WithMany(f => f.RoleFeatures)
                      .HasForeignKey(rf => rf.FeatureId);
            });
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customers");




                entity.Property(c => c.Address)
                      .HasMaxLength(200);

                entity.Property(c => c.LoyaltyNumber)
                      .HasMaxLength(50);

                entity.Property(c => c.LoyaltySince)
                      .HasMaxLength(50);
            });

            // Table for Staff
            modelBuilder.Entity<Staff>(entity =>
            {


                entity.Property(s => s.StaffNumber)
                      .HasMaxLength(30);

                entity.Property(s => s.Position)
                      .HasMaxLength(50);

                entity.Property(s => s.HireDate)
                      .IsRequired();

                entity.Property(s => s.IsManager)
                      .HasDefaultValue(false);
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
            // Offer
            modelBuilder.Entity<Offer>(entity =>
            {
                entity.HasKey(o => o.Id);

                entity.Property(o => o.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(o => o.Description)
                    .HasMaxLength(500);

                entity.Property(o => o.DiscountPercentage)
                    .HasPrecision(5, 2);

                entity.Property(o => o.StartDate).IsRequired();
                entity.Property(o => o.EndDate).IsRequired();
                entity.Property(o => o.IsActive).HasDefaultValue(true);

                entity.HasIndex(o => new { o.StartDate, o.EndDate });

                entity.HasMany(o => o.OfferRooms)
                    .WithOne(or => or.Offer)
                    .HasForeignKey(or => or.OfferId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // OfferRoom
            modelBuilder.Entity<OfferRoom>(entity =>
            {
                entity.HasKey(or => or.Id);

                entity.Property(or => or.SpecialPrice)
                    .HasPrecision(10, 2);

                entity.HasOne(or => or.Offer)
                    .WithMany(o => o.OfferRooms)
                    .HasForeignKey(or => or.OfferId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(or => or.Room)
                    .WithMany(r => r.OfferRooms)
                    .HasForeignKey(or => or.RoomId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RoomConnection>(entity =>
            {
                entity.HasKey(rc => rc.Id);

                entity.Property(rc => rc.ConnectionType)
                      .IsRequired();

                // العلاقة بين Room1 و RoomConnection
                entity.HasOne(rc => rc.Room1)
                    .WithMany(r => r.RoomConnections1)
                    .HasForeignKey(rc => rc.RoomId1)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // العلاقة بين Room2 و RoomConnection
                entity.HasOne(rc => rc.Room2)
                    .WithMany(r => r.RoomConnections2)
                    .HasForeignKey(rc => rc.RoomId2)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();

                // منع التكرار العكسي (1-2 نفس 2-1)
                entity.HasIndex(rc => new { rc.RoomId1, rc.RoomId2 }).IsUnique();
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
               // entity.HasKey(ufr => new { ufr.UserId, ufr.RoomId });
               // entity.HasOne(ufr => ufr.User).WithMany(u => u.FavoriteRooms).HasForeignKey(ufr => ufr.UserId);
                entity.HasOne(ufr => ufr.Room).WithMany(r => r.UserFavorites).HasForeignKey(ufr => ufr.RoomId);
                entity.Property(ufr => ufr.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
            });

            

            modelBuilder.Entity<Review>(entity =>
            {
                entity.HasOne(r => r.Reservation).WithMany().HasForeignKey(r => r.ReservationId).OnDelete(DeleteBehavior.Restrict);
              //  entity.HasOne(r => r.User).WithMany(u => u.Reviews).HasForeignKey(r => r.UserId).OnDelete(DeleteBehavior.Restrict);
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
    }

    public static class SoftDeleteQueryExtension
    {
        public static void AddSoftDeleteQueryFilter(this IMutableEntityType entityData)
        {
            var methodToCall = typeof(SoftDeleteQueryExtension)
                .GetMethod(nameof(GetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
                .MakeGenericMethod(entityData.ClrType);
            var filter = methodToCall.Invoke(null, Array.Empty<object>());
            entityData.SetQueryFilter((LambdaExpression)filter);
        }

        private static LambdaExpression GetSoftDeleteFilter<TEntity>() where TEntity : BaseEntity
        {
            Expression<Func<TEntity, bool>> filter = x => !x.IsDeleted;
            return filter;
        }
    }


    public class HotelDbContextSeed
    {
        public static async Task SeedAdminAsync(HotelDbContext context)
        {
            if (!context.Users.Any())
            {
                var passwordHasher = new PasswordHasher<User>(); 

                var adminRole = context.Roles.FirstOrDefault(r => r.Name == "Admin");
                if (adminRole == null)
                {
                    adminRole = new Role { Name = "Admin" };
                    context.Roles.Add(adminRole);
                    await context.SaveChangesAsync();
                }

                var adminUser = new User
                {
                    Username = "admin",
                    Email = "Admin@Gmail.com",
                    Phone = "01022554525",
                    RoleId = adminRole.Id
                };

                adminUser.PasswordHash = passwordHasher.HashPassword(adminUser, "admin123");

                context.Users.Add(adminUser);
                await context.SaveChangesAsync();
            }
        }
    }


}

