using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    /// <inheritdoc />
    public partial class AdjustsStackProject_AddingActiveField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Confirmed",
                table: "StackProjects");

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "StackProjects",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "StackProjects");

            migrationBuilder.AddColumn<bool>(
                name: "Confirmed",
                table: "StackProjects",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
