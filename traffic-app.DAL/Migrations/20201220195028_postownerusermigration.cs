using Microsoft.EntityFrameworkCore.Migrations;

namespace traffic_app.DAL.Migrations
{
    public partial class postownerusermigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "Owner",
                table: "Posts",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Owner",
                table: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "OwnerUserId",
                table: "Posts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
