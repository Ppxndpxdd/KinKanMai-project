using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KinKanMaiUI.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDescriptionToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Details",
                table: "Order",
                newName: "Description");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Order",
                newName: "Details");
        }
    }
}
