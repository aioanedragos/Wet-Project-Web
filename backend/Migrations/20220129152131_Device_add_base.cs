using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wet_api.Migrations
{
    public partial class Device_add_base : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "lastName",
                table: "Persons",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "firstName",
                table: "Persons",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "email",
                table: "Persons",
                newName: "Email");

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordHash",
                table: "Persons",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "PasswordSalt",
                table: "Persons",
                type: "bytea",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<string>(
                name: "Base",
                table: "Devices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Href",
                table: "Devices",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PasswordHash",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PasswordSalt",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "Base",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "Href",
                table: "Devices");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Persons",
                newName: "lastName");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Persons",
                newName: "firstName");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Persons",
                newName: "email");
        }
    }
}
