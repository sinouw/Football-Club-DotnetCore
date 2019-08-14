using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class resdbset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_AspNetUsers_IdClient",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_Terrains_IdTerrain",
                table: "Reservation");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Reservation_IdReservation",
                table: "Reservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation");

            migrationBuilder.RenameTable(
                name: "Reservation",
                newName: "Reservations");

            migrationBuilder.RenameIndex(
                name: "IX_Reservation_IdTerrain",
                table: "Reservations",
                newName: "IX_Reservations_IdTerrain");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Reservations_IdReservation",
                table: "Reservations",
                column: "IdReservation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                columns: new[] { "IdClient", "IdTerrain" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_AspNetUsers_IdClient",
                table: "Reservations",
                column: "IdClient",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Terrains_IdTerrain",
                table: "Reservations",
                column: "IdTerrain",
                principalTable: "Terrains",
                principalColumn: "IdTerrain",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_AspNetUsers_IdClient",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Terrains_IdTerrain",
                table: "Reservations");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Reservations_IdReservation",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Reservation");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_IdTerrain",
                table: "Reservation",
                newName: "IX_Reservation_IdTerrain");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Reservation_IdReservation",
                table: "Reservation",
                column: "IdReservation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservation",
                table: "Reservation",
                columns: new[] { "IdClient", "IdTerrain" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_AspNetUsers_IdClient",
                table: "Reservation",
                column: "IdClient",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_Terrains_IdTerrain",
                table: "Reservation",
                column: "IdTerrain",
                principalTable: "Terrains",
                principalColumn: "IdTerrain",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
