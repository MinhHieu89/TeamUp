using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamUp.Data.Migrations
{
    public partial class AddUniqueConstraintToNameFieldInTeamsAndUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_AspNetUsers_Name",
                table: "AspNetUsers");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_Name",
                table: "Teams",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Teams_Name",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Name",
                table: "AspNetUsers");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_AspNetUsers_Name",
                table: "AspNetUsers",
                column: "Name");
        }
    }
}
