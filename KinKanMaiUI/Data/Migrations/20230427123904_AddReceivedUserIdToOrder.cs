using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KinKanMaiUI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddReceivedUserIdToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceivedUserId",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedUserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Order");
        }
    }
}
