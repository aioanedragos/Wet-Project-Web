using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wet_api.Migrations
{
    public partial class removedeviceuserid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Devices");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
