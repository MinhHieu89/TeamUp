using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TeamUp.Persistence.Migrations
{
    public partial class AddPhotosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "CoverId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AvatarId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Photos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Teams_CoverId",
                table: "Teams",
                column: "CoverId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_LogoId",
                table: "Teams",
                column: "LogoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Photos_AvatarId",
                table: "AspNetUsers",
                column: "AvatarId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Photos_CoverId",
                table: "Teams",
                column: "CoverId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Teams_Photos_LogoId",
                table: "Teams",
                column: "LogoId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Photos_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Photos_CoverId",
                table: "Teams");

            migrationBuilder.DropForeignKey(
                name: "FK_Teams_Photos_LogoId",
                table: "Teams");

            migrationBuilder.DropTable(
                name: "Photos");

            migrationBuilder.DropIndex(
                name: "IX_Teams_CoverId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_Teams_LogoId",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AvatarId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CoverId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "AvatarId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Teams",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
