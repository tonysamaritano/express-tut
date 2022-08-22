using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VergeDBAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseStations",
                columns: table => new
                {
                    BaseStationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseStations", x => x.BaseStationID);
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
                    FaaId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    FlightHours = table.Column<int>(type: "int", nullable: false),
                    Firmware = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drones", x => x.DroneID);
                });

            migrationBuilder.CreateTable(
                name: "Gateways",
                columns: table => new
                {
                    GatewayID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gateways", x => x.GatewayID);
                });

            migrationBuilder.CreateTable(
                name: "SmartCases",
                columns: table => new
                {
                    SmartCaseID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SmartCases", x => x.SmartCaseID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Performances",
                columns: table => new
                {
                    PerformanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    FlightTime = table.Column<float>(type: "real", nullable: false),
                    StartingBattery = table.Column<float>(type: "real", nullable: false),
                    EndingBattery = table.Column<float>(type: "real", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DroneID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Performances", x => x.PerformanceID);
                    table.ForeignKey(
                        name: "FK_Performances_Drones_DroneID",
                        column: x => x.DroneID,
                        principalTable: "Drones",
                        principalColumn: "DroneID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OwnerID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationMetadata = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Organizations_Users_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assets",
                columns: table => new
                {
                    AssetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeID = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    TableKey = table.Column<int>(type: "int", nullable: false),
                    OrganizationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assets", x => x.AssetID);
                    table.ForeignKey(
                        name: "FK_Assets_Organizations_OrganizationID",
                        column: x => x.OrganizationID,
                        principalTable: "Organizations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Memberships",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<int>(type: "int", nullable: false),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    OrganizationID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memberships", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Memberships_Organizations_OrganizationID",
                        column: x => x.OrganizationID,
                        principalTable: "Organizations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Memberships_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BaseStations",
                column: "BaseStationID",
                value: 1);

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

            migrationBuilder.InsertData(
                table: "Gateways",
                column: "GatewayID",
                value: 1);

            migrationBuilder.InsertData(
                table: "SmartCases",
                column: "SmartCaseID",
                value: 1);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "Tony@vergeaero.com", "Drones", "Tony" },
                    { 2, "Gabe@gabe.gov", "Gabe", "Gabe" },
                    { 3, "ronalddonald@mcd.gov", "Donald", "Ronald" },
                    { 4, "jeremy@strictly.net", "j123", "Jeremy" },
                    { 5, "johnny@go.net", "johnny", "John" }
                });

            migrationBuilder.InsertData(
                table: "Organizations",
                columns: new[] { "ID", "Address", "Name", "OrganizationMetadata", "OwnerID" },
                values: new object[,]
                {
                    { 1, "7905 Browning Road, Pensauken, NJ", "Verge Aero", "", 1 },
                    { 2, "756 S. Glasgow Avenue Inglewood, CA 90301", "Strictly", "", 4 },
                    { 3, "1144 Tampa Road, Palm Harbor, FL 34683", "Go Agency", "", 5 }
                });

            migrationBuilder.InsertData(
                table: "Performances",
                columns: new[] { "PerformanceID", "Date", "DroneID", "EndingBattery", "FlightTime", "Slot", "StartingBattery" },
                values: new object[,]
                {
                    { 1, new DateTime(2022, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 11.6f, 10f, 188, 12.6f },
                    { 2, new DateTime(2022, 7, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 11.4f, 10.03f, 23, 12.6f },
                    { 3, new DateTime(2022, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 12.4f, 2f, 26, 12.6f }
                });

            migrationBuilder.InsertData(
                table: "Assets",
                columns: new[] { "AssetID", "OrganizationID", "TableKey", "TypeID" },
                values: new object[,]
                {
                    { 1, 1, 3, 0m },
                    { 2, 3, 1, 0m },
                    { 3, 1, 4, 0m },
                    { 4, 2, 2, 0m },
                    { 5, 1, 3, 1m },
                    { 6, 3, 1, 1m },
                    { 7, 1, 2, 1m },
                    { 8, 1, 1, 2m },
                    { 9, 3, 1, 4m },
                    { 10, 2, 1, 8m }
                });

            migrationBuilder.InsertData(
                table: "Memberships",
                columns: new[] { "ID", "OrganizationID", "Position", "Role", "UserID" },
                values: new object[,]
                {
                    { 1, 1, "Engineer", 6, 1 },
                    { 2, 1, "Intern", 2, 2 },
                    { 3, 2, "Sales", -1, 3 },
                    { 4, 2, "CEO", 5, 4 },
                    { 5, 3, "CEO", 5, 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assets_OrganizationID",
                table: "Assets",
                column: "OrganizationID");

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

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_OrganizationID",
                table: "Memberships",
                column: "OrganizationID");

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_UserID",
                table: "Memberships",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OwnerID",
                table: "Organizations",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_Performances_DroneID",
                table: "Performances",
                column: "DroneID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assets");

            migrationBuilder.DropTable(
                name: "BaseStations");

            migrationBuilder.DropTable(
                name: "Batteries");

            migrationBuilder.DropTable(
                name: "Gateways");

            migrationBuilder.DropTable(
                name: "Memberships");

            migrationBuilder.DropTable(
                name: "Performances");

            migrationBuilder.DropTable(
                name: "SmartCases");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "Drones");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
