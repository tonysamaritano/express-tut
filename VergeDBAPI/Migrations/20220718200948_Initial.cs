using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VergeDBAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeID = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TableKey = table.Column<int>(type: "int", nullable: false),
                    OrganizationID = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetID);
                });

            migrationBuilder.CreateTable(
                name: "Batteries",
                columns: table => new
                {
                    BatteryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BatteryCycles = table.Column<int>(type: "int", nullable: false),
                    BatteryType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Batteries", x => x.BatteryID);
                });

            migrationBuilder.CreateTable(
                name: "Drones",
                columns: table => new
                {
                    DroneID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DroneUID = table.Column<int>(type: "int", nullable: false),
                    FaaId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    FlightHours = table.Column<int>(type: "int", nullable: false),
                    Firmware = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drones", x => x.DroneID);
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "AssetID", "OrganizationID", "TableKey", "TypeID" },
                values: new object[,]
                {
                    { 1, (short)-1, 3, 0m },
                    { 2, (short)2, 1, 0m },
                    { 3, (short)0, 4, 0m },
                    { 4, (short)-1, 2, 0m },
                    { 5, (short)1, 3, 1m },
                    { 6, (short)1, 1, 1m },
                    { 7, (short)0, 2, 1m }
                });

            migrationBuilder.InsertData(
                table: "Batteries",
                columns: new[] { "BatteryID", "BatteryCycles", "BatteryType" },
                values: new object[,]
                {
                    { 1, 20, 0 },
                    { 2, 331, 1 },
                    { 3, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Drones",
                columns: new[] { "DroneID", "DroneUID", "FaaId", "Firmware", "FlightHours" },
                values: new object[,]
                {
                    { 1, 1001, "GTF43UH3S5", "1.1.1.3", 2000 },
                    { 2, 392, "NM7A0B1P2S", "", 191 },
                    { 3, 2312, null, "", 600 },
                    { 4, 912, null, "", 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_TypeID_TableKey",
                table: "Assets",
                columns: new[] { "TypeID", "TableKey" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drones_DroneUID",
                table: "Drones",
                column: "DroneUID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Drones_FaaId",
                table: "Drones",
                column: "FaaId",
                unique: true,
                filter: "[FaaId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "Batteries");

            migrationBuilder.DropTable(
                name: "Drones");
        }
    }
}
