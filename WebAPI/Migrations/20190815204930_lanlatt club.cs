using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class lanlattclub : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "lat",
                table: "Clubs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "lng",
                table: "Clubs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "lat",
                table: "Clubs");

            migrationBuilder.DropColumn(
                name: "lng",
                table: "Clubs");
        }
    }
}
