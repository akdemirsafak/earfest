using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace earfest.API.Migrations
{
    /// <inheritdoc />
    public partial class OrderUpdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CardNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MaskedHolderName",
                table: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CardNumber",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MaskedHolderName",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
