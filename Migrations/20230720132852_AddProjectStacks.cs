using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    /// <inheritdoc />
    public partial class AddProjectStacks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Stacks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StackProjects",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(type: "uuid", nullable: false),
                    StackId = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Confirmed = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StackProjects", x => new { x.StackId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_StackProjects_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StackProjects_Stacks_StackId",
                        column: x => x.StackId,
                        principalTable: "Stacks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StackProjects_ProjectId",
                table: "StackProjects",
                column: "ProjectId");
            
            migrationBuilder.InsertData(
                table: "Stacks",
                columns: new[] { "Id", "Name"},
                values: new object[,]
                {
                    { "cs", "C#" },
                    { "css", "CSS" },
                    { "cloudformation", "CloudFormation" },
                    { "docker", "Docker" },
                    { "flex", "Flex" },
                    { "go", "Go" },
                    { "web", "HTML" },
                    { "json", "JSON" },
                    { "jsp", "JSP" },
                    { "java", "Java" },
                    { "js", "JavaScript" },
                    { "kotlin", "Kotlin" },
                    { "kubernetes", "Kubernetes" },
                    { "neutral", "Neutral" },
                    { "php", "PHP" },
                    { "py", "Python" },
                    { "ruby", "Ruby" },
                    { "scala", "Scala" },
                    { "secrets", "Secrets" },
                    { "terraform", "Terraform" },
                    { "text", "Text" },
                    { "ts", "TypeScript" },
                    { "vbnet", "VB.NET" },
                    { "xml", "XML" },
                    { "yaml", "YAML" }     
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StackProjects");

            migrationBuilder.DropTable(
                name: "Stacks");
        }
    }
}
