using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class postal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Clubs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Postal",
                table: "Clubs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Clubs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "Clubs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "Postal",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "Clubs");
        }
    }
}
