using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using wet_api.Data;

#nullable disable

namespace wet_api.Migrations
{
    public partial class devicesjsonactionsproperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Dictionary<string, wet_api.Data.Action>>(
                name: "Actions",
                table: "Devices",
                type: "jsonb",
                nullable: false);

            migrationBuilder.AddColumn<Dictionary<string, Property>>(
                name: "Properties",
                table: "Devices",
                type: "jsonb",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Actions",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Properties",
                table: "Devices");
        }
    }
}
