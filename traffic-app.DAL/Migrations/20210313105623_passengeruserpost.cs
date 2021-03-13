using Microsoft.EntityFrameworkCore.Migrations;

namespace traffic_app.DAL.Migrations
{
    public partial class passengeruserpost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountOfPassenger",
                table: "OnTheWayPassengerPosts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PaymentForEachPassenger",
                table: "OnTheWayPassengerPosts",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "OnTheWayPassengerPosts",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CountOfPassenger",
                table: "OnTheWayPassengerPosts");

            migrationBuilder.DropColumn(
                name: "PaymentForEachPassenger",
                table: "OnTheWayPassengerPosts");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "OnTheWayPassengerPosts");
        }
    }
}
