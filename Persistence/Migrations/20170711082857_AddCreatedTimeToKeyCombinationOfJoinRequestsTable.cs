using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamUp.Persistence.Migrations
{
    public partial class AddCreatedTimeToKeyCombinationOfJoinRequestsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinRequests",
                table: "JoinRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinRequests",
                table: "JoinRequests",
                columns: new[] { "UserId", "TeamId", "CreatedTime" });

            migrationBuilder.AddForeignKey(
                name: "FK_JoinRequests_AspNetUsers_UserId",
                table: "JoinRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoinRequests_AspNetUsers_UserId",
                table: "JoinRequests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JoinRequests",
                table: "JoinRequests");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JoinRequests",
                table: "JoinRequests",
                columns: new[] { "UserId", "TeamId" });
        }
    }
}
