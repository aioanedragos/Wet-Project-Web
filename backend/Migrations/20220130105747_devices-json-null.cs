using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using wet_api.Data;

#nullable disable

namespace wet_api.Migrations
{
    public partial class devicesjsonnull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Dictionary<string, Property>>(
                name: "Properties",
                table: "Devices",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(Dictionary<string, Property>),
                oldType: "jsonb");

            migrationBuilder.AlterColumn<Dictionary<string, wet_api.Data.Action>>(
                name: "Actions",
                table: "Devices",
                type: "jsonb",
                nullable: true,
                oldClrType: typeof(Dictionary<string, wet_api.Data.Action>),
                oldType: "jsonb");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Dictionary<string, Property>>(
                name: "Properties",
                table: "Devices",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(Dictionary<string, Property>),
                oldType: "jsonb",
                oldNullable: true);

            migrationBuilder.AlterColumn<Dictionary<string, wet_api.Data.Action>>(
                name: "Actions",
                table: "Devices",
                type: "jsonb",
                nullable: false,
                oldClrType: typeof(Dictionary<string, wet_api.Data.Action>),
                oldType: "jsonb",
                oldNullable: true);
        }
    }
}
