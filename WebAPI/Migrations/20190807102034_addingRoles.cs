using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class addingRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    IdClub = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Phone = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    OpeningTime = table.Column<DateTime>(nullable: false),
                    ClosingTime = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ClubAdminId = table.Column<Guid>(nullable: false),
                    ClubAdminId1 = table.Column<string>(nullable: true),
                    SuperAdminId = table.Column<Guid>(nullable: false),
                    SuperAdminId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.IdClub);
                    table.ForeignKey(
                        name: "FK_Clubs_AspNetUsers_ClubAdminId1",
                        column: x => x.ClubAdminId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Clubs_AspNetUsers_SuperAdminId1",
                        column: x => x.SuperAdminId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Terrains",
                columns: table => new
                {
                    IdTerrain = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    Free = table.Column<bool>(nullable: false),
                    IdClub = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terrains", x => x.IdTerrain);
                    table.ForeignKey(
                        name: "FK_Terrains_Clubs_IdClub",
                        column: x => x.IdClub,
                        principalTable: "Clubs",
                        principalColumn: "IdClub",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    IdClient = table.Column<string>(nullable: false),
                    IdTerrain = table.Column<Guid>(nullable: false),
                    IdReservation = table.Column<Guid>(nullable: false),
                    StartRes = table.Column<DateTime>(nullable: false),
                    EndRes = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => new { x.IdClient, x.IdTerrain });
                    table.UniqueConstraint("AK_Reservation_IdReservation", x => x.IdReservation);
                    table.ForeignKey(
                        name: "FK_Reservation_AspNetUsers_IdClient",
                        column: x => x.IdClient,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservation_Terrains_IdTerrain",
                        column: x => x.IdTerrain,
                        principalTable: "Terrains",
                        principalColumn: "IdTerrain",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "ClubAdmin", "CLUBADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3", null, "Client", "CLIENT" });

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_ClubAdminId1",
                table: "Clubs",
                column: "ClubAdminId1");

            migrationBuilder.CreateIndex(
                name: "IX_Clubs_SuperAdminId1",
                table: "Clubs",
                column: "SuperAdminId1");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_IdTerrain",
                table: "Reservation",
                column: "IdTerrain");

            migrationBuilder.CreateIndex(
                name: "IX_Terrains_IdClub",
                table: "Terrains",
                column: "IdClub");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservation");

            migrationBuilder.DropTable(
                name: "Terrains");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "Name", "NormalizedName" },
                values: new object[] { "Admin", "ADMIN" });
        }
    }
}
