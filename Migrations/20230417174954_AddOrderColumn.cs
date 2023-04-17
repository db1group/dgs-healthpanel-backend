using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Pillars",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Column",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Pillars");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Column");
        }
    }
}
