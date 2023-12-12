using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    /// <inheritdoc />
    public partial class DB1CLIKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "UseDB1CLI",
                table: "Projects",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "KeysDB1CLI",
                columns: table => new
                {
                    Key = table.Column<Guid>(type: "uuid", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    LeadId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeysDB1CLI", x => x.Key);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KeysDB1CLI");

            migrationBuilder.DropColumn(
                name: "UseDB1CLI",
                table: "Projects");
        }
    }
}
