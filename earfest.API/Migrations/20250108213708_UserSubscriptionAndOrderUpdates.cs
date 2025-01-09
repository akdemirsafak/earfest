using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace earfest.API.Migrations
{
    /// <inheritdoc />
    public partial class UserSubscriptionAndOrderUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Token",
                table: "Orders");

            migrationBuilder.AddColumn<string>(
                name: "CardToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentProviderCustomerId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PaymentProviderCustomerId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Token",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
