using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace vergedb.Migrations
{
    public partial class AddOwners : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerID",
                table: "Drone",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Owner",
                columns: table => new
                {
                    OwnerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumDrones = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owner", x => x.OwnerID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Drone_OwnerID",
                table: "Drone",
                column: "OwnerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Drone_Owner_OwnerID",
                table: "Drone",
                column: "OwnerID",
                principalTable: "Owner",
                principalColumn: "OwnerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Drone_Owner_OwnerID",
                table: "Drone");

            migrationBuilder.DropTable(
                name: "Owner");

            migrationBuilder.DropIndex(
                name: "IX_Drone_OwnerID",
                table: "Drone");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "Drone");
        }
    }
}
