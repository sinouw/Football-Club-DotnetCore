using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class trucdriver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ClubAdminId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClubAdminId1",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lat",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Lng",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4", null, "TruckDriver", "TRUCKDRIVER" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClubAdminId1",
                table: "AspNetUsers",
                column: "ClubAdminId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ClubAdminId1",
                table: "AspNetUsers",
                column: "ClubAdminId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_ClubAdminId1",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClubAdminId1",
                table: "AspNetUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4");

            migrationBuilder.DropColumn(
                name: "ClubAdminId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClubAdminId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Lat",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Lng",
                table: "AspNetUsers");
        }
    }
}
