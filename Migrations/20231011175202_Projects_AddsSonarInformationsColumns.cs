using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    /// <inheritdoc />
    public partial class Projects_AddsSonarInformationsColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MetricsCollectorProjectName",
                newName: "SonarName",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "SonarToken",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SonarUrl",
                table: "Projects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                newName: "MetricsCollectorProjectName",
                name: "SonarName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SonarToken",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "SonarUrl",
                table: "Projects");
        }
    }
}
