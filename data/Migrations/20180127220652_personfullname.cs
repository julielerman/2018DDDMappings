using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace data.Migrations
{
    public partial class personfullname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NameFactory_First",
                table: "Manager",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameFactory_Last",
                table: "Manager",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameFactory_First",
                table: "Player",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NameFactory_Last",
                table: "Player",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NameFactory_First",
                table: "Manager");

            migrationBuilder.DropColumn(
                name: "NameFactory_Last",
                table: "Manager");

            migrationBuilder.DropColumn(
                name: "NameFactory_First",
                table: "Player");

            migrationBuilder.DropColumn(
                name: "NameFactory_Last",
                table: "Player");
        }
    }
}
