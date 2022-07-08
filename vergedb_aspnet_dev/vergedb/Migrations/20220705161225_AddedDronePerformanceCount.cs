using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vergedb.Migrations
{
    public partial class AddedDronePerformanceCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PerformanceCount",
                table: "Drone",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerformanceCount",
                table: "Drone");
        }
    }
}
