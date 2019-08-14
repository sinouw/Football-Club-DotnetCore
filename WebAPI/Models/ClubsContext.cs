using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.Models.Auth.Roles;
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
                    new { Id = "3", Name = "Client", NormalizedName = "CLIENT" }
                );

            builder.Entity<Reservation>()
            .HasKey(bc => new { bc.IdClient, bc.IdTerrain });

            builder.Entity<Reservation>()
            .HasOne(bc => bc.Client)
            .WithMany(b => b.Reservations)
            .HasForeignKey(bc => bc.IdClient);

            builder.Entity<Reservation>()
            .HasOne(bc => bc.Terrain)
            .WithMany(c => c.Reservations)
            .HasForeignKey(bc => bc.IdTerrain);

            builder.Entity<Club>()
            .HasMany(c => c.Terrains)
            .WithOne(t => t.club);

            builder.Entity<ClubAdmin>()
            .HasMany(bc => bc.Clubs)
            .WithOne(c => c.ClubAdmin);

            builder.Entity<SuperAdmin>()
            .HasMany(bc => bc.Clubs)
            .WithOne(c => c.SuperAdmin);

            builder.Entity<Terrain>()
               .HasMany(t => t.Images)
               .WithOne(e => e.Terrain);

        }

        public DbSet<User> ApplicationUsers { get; set; }
        public DbSet<SuperAdmin> SuperAdmins { get; set; }
        public DbSet<ClubAdmin> ClubAdmins { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Terrain> Terrains { get; set; }
        public DbSet<Club> Clubs { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Image> Images { get; set; }

    }
}
