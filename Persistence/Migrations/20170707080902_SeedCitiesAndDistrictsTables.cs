using Microsoft.EntityFrameworkCore.Migrations;

namespace TeamUp.Persistence.Migrations
{
    public partial class SeedCitiesAndDistrictsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Cities (Name) VALUES (N'Hà Nội')");
            migrationBuilder.Sql("INSERT INTO Cities (Name) VALUES (N'Đà Nẵng')");
            migrationBuilder.Sql("INSERT INTO Cities (Name) VALUES (N'TP Hồ Chí Minh')");

            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'Hà Nội'), N'Ba Đình')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'Hà Nội'), N'Cầu Giấy')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'Hà Nội'), N'Long Biên')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'Hà Nội'), N'Tây Hồ')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'Hà Nội'), N'Hoàn Kiếm')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'Đà Nẵng'), N'Thanh Khuê')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'Đà Nẵng'), N'Liên Chiểu')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'Đà Nẵng'), N'Hải Châu')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'Đà Nẵng'), N'Hòa Vang')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'Đà Nẵng'), N'Sơn Trà')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'TP Hồ Chí Minh'), N'Quận 1')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'TP Hồ Chí Minh'), N'Quận 2')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'TP Hồ Chí Minh'), N'Quận 3')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'TP Hồ Chí Minh'), N'Quận 4')");
            migrationBuilder.Sql("INSERT INTO Districts (CityId, Name) VALUES ((SELECT ID FROM Cities WHERE Name=N'TP Hồ Chí Minh'), N'Quận 5')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Cities WHERE Name IN (N'Hà Nội', N'Đà Nẵng', N'TP Hồ Chí Minh')");
        }
    }
}
