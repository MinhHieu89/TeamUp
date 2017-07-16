using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamUp.Persistence.Migrations
{
    public partial class AddPositionsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.Sql("INSERT INTO Positions (Id, Name) VALUES (1, 'GK')");
            migrationBuilder.Sql("INSERT INTO Positions (Id, Name) VALUES (2, 'CB')");
            migrationBuilder.Sql("INSERT INTO Positions (Id, Name) VALUES (3, 'LB')");
            migrationBuilder.Sql("INSERT INTO Positions (Id, Name) VALUES (4, 'RB')");
            migrationBuilder.Sql("INSERT INTO Positions (Id, Name) VALUES (5, 'CDM')");
            migrationBuilder.Sql("INSERT INTO Positions (Id, Name) VALUES (6, 'CM')");
            migrationBuilder.Sql("INSERT INTO Positions (Id, Name) VALUES (7, 'LM')");
            migrationBuilder.Sql("INSERT INTO Positions (Id, Name) VALUES (8, 'RM')");
            migrationBuilder.Sql("INSERT INTO Positions (Id, Name) VALUES (9, 'CAM')");
            migrationBuilder.Sql("INSERT INTO Positions (Id, Name) VALUES (10, 'CF')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Positions");
        }
    }
}
