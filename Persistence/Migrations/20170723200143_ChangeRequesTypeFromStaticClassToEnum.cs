using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamUp.Persistence.Migrations
{
    public partial class ChangeRequesTypeFromStaticClassToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "RequestType",
                table: "Requests",
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 10);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestType",
                table: "Requests",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
