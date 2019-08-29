using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Auth.Roles;
using WebAPI.Models.Roles;
using WebAPI.Models.Sports;

namespace WebAPI.Models
{
    public class ClubsContext : IdentityDbContext
    {
        public ClubsContext(DbContextOptions options) : base(options)
        {

        }

        // Creating Roles for the application
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                    new { Id = "1", Name = "SuperAdmin", NormalizedName = "SUPERADMIN" },
                    new { Id = "2", Name = "ClubAdmin", NormalizedName = "CLUBADMIN" },
                    new { Id = "3", Name = "Client", NormalizedName = "CLIENT" },
                    new { Id = "4", Name = "TruckDriver", NormalizedName = "TRUCKDRIVER" }
                );

            builder.Entity<Client>()
            .HasMany(c => c.Reservations)
            .WithOne(t => t.Client)
            .HasForeignKey(bc => bc.IdClient);

            builder.Entity<Terrain>()
            .HasMany(c => c.Reservations)
            .WithOne(t => t.Terrain)
            .HasForeignKey(bc => bc.IdTerrain);

            builder.Entity<Club>()
            .HasMany(c => c.Terrains)
            .WithOne(t => t.club);

            builder.Entity<ClubAdmin>()
            .HasMany(bc => bc.Clubs)
            .WithOne(c => c.ClubAdmin);
            //.HasForeignKey(c=>c.ClubAdminId);

            builder.Entity<ClubAdmin>()
            .HasMany(bc => bc.TruckDrivers)
            .WithOne(c => c.ClubAdmin);

            builder.Entity<SuperAdmin>()
            .HasMany(bc => bc.Clubs)
            .WithOne(c => c.SuperAdmin);
            //.HasForeignKey(c => c.SuperAdminId);

            builder.Entity<Terrain>()
               .HasMany(t => t.Images)
               .WithOne(e => e.Terrain);

        }

        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<ClubAdmin> ClubAdmins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<TruckDriver> TruckDrivers { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Image> Images { get; set; }

    }
}
