using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace traffic_app.DAL.Migrations
{
    public partial class driverpost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OnTheWayDriverPosts",
                columns: table => new
                {
                    OnTheWayDriverPostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    FromPlace = table.Column<string>(nullable: false),
                    ToPlace = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    CanSmoke = table.Column<bool>(nullable: false),
                    Payment = table.Column<string>(nullable: true),
                    CanTakeLuggage = table.Column<bool>(nullable: false),
                    CountOfEmptyPlace = table.Column<int>(nullable: false),
                    CarModel = table.Column<string>(nullable: true),
                    CarImageUrl = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnTheWayDriverPosts", x => x.OnTheWayDriverPostId);
                    table.ForeignKey(
                        name: "FK_OnTheWayDriverPosts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OnTheWayPassengerPosts",
                columns: table => new
                {
                    OnTheWayPassengerPostId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    FromPlace = table.Column<string>(nullable: false),
                    ToPlace = table.Column<string>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    DeletedAt = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OnTheWayPassengerPosts", x => x.OnTheWayPassengerPostId);
                    table.ForeignKey(
                        name: "FK_OnTheWayPassengerPosts_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OnTheWayDriverPosts_UserId",
                table: "OnTheWayDriverPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_OnTheWayPassengerPosts_UserId",
                table: "OnTheWayPassengerPosts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OnTheWayDriverPosts");

            migrationBuilder.DropTable(
                name: "OnTheWayPassengerPosts");
        }
    }
}
