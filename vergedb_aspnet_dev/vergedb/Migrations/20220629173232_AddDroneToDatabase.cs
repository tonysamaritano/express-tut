using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vergedb.Migrations
{
    public partial class AddDroneToDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FaaId",
                table: "Drone");

            migrationBuilder.DropColumn(
                name: "GPSLocation",
                table: "Drone");

            migrationBuilder.DropColumn(
                name: "OwnerName",
                table: "Drone");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FaaId",
                table: "Drone",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GPSLocation",
                table: "Drone",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OwnerName",
                table: "Drone",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
