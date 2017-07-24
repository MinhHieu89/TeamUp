using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamUp.Persistence.Migrations
{
    public partial class ChangeColumnNameOfRequestsTableFromRequestTypeToType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestType",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Requests",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Requests");

            migrationBuilder.AddColumn<int>(
                name: "RequestType",
                table: "Requests",
                nullable: false,
                defaultValue: 0);
        }
    }
}
