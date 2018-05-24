using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace data.Migrations
{
    public partial class managerCurrentTeamIdToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "CurrentTeamId",
                table: "Managers",
                nullable: false,
                oldClrType: typeof(int));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CurrentTeamId",
                table: "Managers",
                nullable: false,
                oldClrType: typeof(Guid));
        }
    }
}
