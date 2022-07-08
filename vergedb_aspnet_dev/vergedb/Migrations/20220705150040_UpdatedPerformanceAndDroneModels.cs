using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vergedb.Migrations
{
    public partial class UpdatedPerformanceAndDroneModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DroneUID",
                table: "Performance");

            migrationBuilder.AddColumn<float>(
                name: "EndingBattery",
                table: "Performance",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "FlightTime",
                table: "Performance",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "StartingBattery",
                table: "Performance",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<string>(
                name: "FaaId",
                table: "Drone",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PixHardware",
                table: "Drone",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EndingBattery",
                table: "Performance");

            migrationBuilder.DropColumn(
                name: "FlightTime",
                table: "Performance");

            migrationBuilder.DropColumn(
                name: "StartingBattery",
                table: "Performance");

            migrationBuilder.DropColumn(
                name: "FaaId",
                table: "Drone");

            migrationBuilder.DropColumn(
                name: "PixHardware",
                table: "Drone");

            migrationBuilder.AddColumn<int>(
                name: "DroneUID",
                table: "Performance",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
