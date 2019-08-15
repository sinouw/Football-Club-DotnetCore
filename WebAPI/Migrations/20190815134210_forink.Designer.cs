﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI.Models;

namespace WebAPI.Migrations
{
    [DbContext(typeof(ClubsContext))]
    [Migration("20190815134210_forink")]
    partial class forink
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            Name = "SuperAdmin",
                            NormalizedName = "SUPERADMIN"
                        },
                        new
                        {
                            Id = "2",
                            Name = "ClubAdmin",
                            NormalizedName = "CLUBADMIN"
                        },
                        new
                        {
                            Id = "3",
                            Name = "Client",
                            NormalizedName = "CLIENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasMaxLength(128);

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("WebAPI.Models.Club", b =>
                {
                    b.Property<Guid>("IdClub")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<DateTime>("ClosingTime");

                    b.Property<Guid>("ClubAdminId");

                    b.Property<string>("ClubAdminId1");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<DateTime>("OpeningTime");

                    b.Property<int>("Phone");

                    b.Property<Guid>("SuperAdminId");

                    b.Property<string>("SuperAdminId1");

                    b.HasKey("IdClub");

                    b.HasIndex("ClubAdminId1");

                    b.HasIndex("SuperAdminId1");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("WebAPI.Models.Reservation", b =>
                {
                    b.Property<Guid>("IdReservation")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EndRes");

                    b.Property<string>("IdClient");

                    b.Property<Guid>("IdTerrain");

                    b.Property<DateTime>("StartRes");

                    b.Property<string>("status");

                    b.HasKey("IdReservation");

                    b.HasIndex("IdClient");

                    b.HasIndex("IdTerrain");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("WebAPI.Models.Sports.Image", b =>
                {
                    b.Property<Guid>("IdImage")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("IdTerrain");

                    b.Property<string>("ImageName");

                    b.HasKey("IdImage");

                    b.HasIndex("IdTerrain");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("WebAPI.Models.Terrain", b =>
                {
                    b.Property<Guid>("IdTerrain")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Free");

                    b.Property<Guid>("IdClub");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<double>("Price");

                    b.Property<string>("Type")
                        .IsRequired();

                    b.HasKey("IdTerrain");

                    b.HasIndex("IdClub");

                    b.ToTable("Terrains");
                });

            modelBuilder.Entity("WebAPI.Models.User", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Gender");

                    b.Property<bool>("IsActive");

                    b.HasDiscriminator().HasValue("User");
                });

            modelBuilder.Entity("WebAPI.Models.Auth.Roles.Client", b =>
                {
                    b.HasBaseType("WebAPI.Models.User");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("WebAPI.Models.Auth.Roles.ClubAdmin", b =>
                {
                    b.HasBaseType("WebAPI.Models.User");

                    b.HasDiscriminator().HasValue("ClubAdmin");
                });

            modelBuilder.Entity("WebAPI.Models.Auth.Roles.SuperAdmin", b =>
                {
                    b.HasBaseType("WebAPI.Models.User");

                    b.HasDiscriminator().HasValue("SuperAdmin");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.Club", b =>
                {
                    b.HasOne("WebAPI.Models.Auth.Roles.ClubAdmin", "ClubAdmin")
                        .WithMany("Clubs")
                        .HasForeignKey("ClubAdminId1");

                    b.HasOne("WebAPI.Models.Auth.Roles.SuperAdmin", "SuperAdmin")
                        .WithMany("Clubs")
                        .HasForeignKey("SuperAdminId1");
                });

            modelBuilder.Entity("WebAPI.Models.Reservation", b =>
                {
                    b.HasOne("WebAPI.Models.Auth.Roles.Client", "Client")
                        .WithMany("Reservations")
                        .HasForeignKey("IdClient");

                    b.HasOne("WebAPI.Models.Terrain", "Terrain")
                        .WithMany("Reservations")
                        .HasForeignKey("IdTerrain")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.Sports.Image", b =>
                {
                    b.HasOne("WebAPI.Models.Terrain", "Terrain")
                        .WithMany("Images")
                        .HasForeignKey("IdTerrain")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebAPI.Models.Terrain", b =>
                {
                    b.HasOne("WebAPI.Models.Club", "club")
                        .WithMany("Terrains")
                        .HasForeignKey("IdClub")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
