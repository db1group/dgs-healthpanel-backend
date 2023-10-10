using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    public partial class RenameStackDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE public.\"Stacks\" SET \"Name\"='Golang' WHERE \"Id\"='go'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE public.\"Stacks\" SET \"Name\"='Go' WHERE \"Id\"='go'");    
        }
    }
}
