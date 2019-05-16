using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    CityId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Name = table.Column<string>(nullable: true),
                    County = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    Population = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    Email = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Rating = table.Column<double>(nullable: false),
                    RateAmount = table.Column<long>(nullable: false),
                    Photo = table.Column<byte[]>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Car = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Place",
                columns: table => new
                {
                    PlaceId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    CityId = table.Column<int>(nullable: true),
                    PlaceInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Place", x => x.PlaceId);
                    table.ForeignKey(
                        name: "FK_Place_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "CityId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ride",
                columns: table => new
                {
                    RideId = table.Column<int>(nullable: false)
                        .Annotation("MySQL:AutoIncrement", true),
                    OwnerId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    FromPlaceId = table.Column<int>(nullable: true),
                    ToPlaceId = table.Column<int>(nullable: true),
                    NumOfSeats = table.Column<int>(nullable: false),
                    BookedSeats = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ride", x => x.RideId);
                    table.ForeignKey(
                        name: "FK_Ride_Place_FromPlaceId",
                        column: x => x.FromPlaceId,
                        principalTable: "Place",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ride_Place_ToPlaceId",
                        column: x => x.ToPlaceId,
                        principalTable: "Place",
                        principalColumn: "PlaceId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PassengerRides",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false),
                    RideId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassengerRides", x => new { x.UserId, x.RideId });
                    table.ForeignKey(
                        name: "FK_PassengerRides_Ride_RideId",
                        column: x => x.RideId,
                        principalTable: "Ride",
                        principalColumn: "RideId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassengerRides_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PassengerRides_RideId",
                table: "PassengerRides",
                column: "RideId");

            migrationBuilder.CreateIndex(
                name: "IX_Place_CityId",
                table: "Place",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Ride_FromPlaceId",
                table: "Ride",
                column: "FromPlaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Ride_ToPlaceId",
                table: "Ride",
                column: "ToPlaceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PassengerRides");

            migrationBuilder.DropTable(
                name: "Ride");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Place");

            migrationBuilder.DropTable(
                name: "City");
        }
    }
}
