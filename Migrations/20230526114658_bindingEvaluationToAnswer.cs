using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    /// <inheritdoc />
    public partial class bindingEvaluationToAnswer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AnswerId",
                table: "Evaluations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "EvaluationId",
                table: "Answers",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Answers_EvaluationId",
                table: "Answers",
                column: "EvaluationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Evaluations_EvaluationId",
                table: "Answers",
                column: "EvaluationId",
                principalTable: "Evaluations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Evaluations_EvaluationId",
                table: "Answers");

            migrationBuilder.DropIndex(
                name: "IX_Answers_EvaluationId",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "Evaluations");

            migrationBuilder.DropColumn(
                name: "EvaluationId",
                table: "Answers");
        }
    }
}
