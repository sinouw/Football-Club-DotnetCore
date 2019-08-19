using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class ReservationDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndRes",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "StartRes",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "resDay",
                table: "Reservations");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndReservation",
                table: "Reservations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartReservation",
                table: "Reservations",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndReservation",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "StartReservation",
                table: "Reservations");

            migrationBuilder.AddColumn<string>(
                name: "EndRes",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StartRes",
                table: "Reservations",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "resDay",
                table: "Reservations",
                nullable: true);
        }
    }
}
