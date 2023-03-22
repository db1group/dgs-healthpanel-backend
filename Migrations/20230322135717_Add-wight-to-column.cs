using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    /// <inheritdoc />
    public partial class Addwighttocolumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "Weight",
                table: "Column",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Column");
        }
    }
}
