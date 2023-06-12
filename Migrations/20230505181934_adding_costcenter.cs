using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    /// <inheritdoc />
    public partial class adding_costcenter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CostCenter",
                table: "Projects");

            migrationBuilder.AddColumn<Guid>(
                name: "CostCenterId",
                table: "Projects",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "CostCenters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CostCenters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CostCenterId",
                table: "Projects",
                column: "CostCenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_CostCenters_CostCenterId",
                table: "Projects",
                column: "CostCenterId",
                principalTable: "CostCenters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_CostCenters_CostCenterId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "CostCenters");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CostCenterId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CostCenterId",
                table: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "CostCenter",
                table: "Projects",
                type: "text",
                nullable: true);
        }
    }
}
