using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vergedb.Migrations
{
    public partial class NewColumnNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Drone",
                newName: "DroneID");

            migrationBuilder.CreateTable(
                name: "Performance",
                columns: table => new
                {
                    PerformanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    DroneUID = table.Column<int>(type: "int", nullable: false),
                    DroneID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performance", x => x.PerformanceID);
                    table.ForeignKey(
                        name: "FK_Performance_Drone_DroneID",
                        column: x => x.DroneID,
                        principalTable: "Drone",
                        principalColumn: "DroneID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Performance_DroneID",
                table: "Performance",
                column: "DroneID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Performance");

            migrationBuilder.RenameColumn(
                name: "DroneID",
                table: "Drone",
                newName: "Id");
        }
    }
}
