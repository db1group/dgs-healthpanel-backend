using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Db1HealthPanelBack.Migrations
{
    /// <inheritdoc />
    public partial class addtablesPillarsQuestions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                table: "Answers",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "InTraining",
                table: "Leads",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AdditionalData",
                table: "Column",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Answers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Answers",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "AnswerPillars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PillarId = table.Column<Guid>(type: "uuid", nullable: false),
                    AdditionalData = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AnswerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswerPillars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswerPillars_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnswerPillars_Pillars_PillarId",
                        column: x => x.PillarId,
                        principalTable: "Pillars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswersQuestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    QuestionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AnswerId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersQuestions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswersQuestions_Answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "Answers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AnswersQuestions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswerPillars_AnswerId",
                table: "AnswerPillars",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswerPillars_PillarId",
                table: "AnswerPillars",
                column: "PillarId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswersQuestions_AnswerId",
                table: "AnswersQuestions",
                column: "AnswerId");

            migrationBuilder.CreateIndex(
                name: "IX_AnswersQuestions_QuestionId",
                table: "AnswersQuestions",
                column: "QuestionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswerPillars");

            migrationBuilder.DropTable(
                name: "AnswersQuestions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Answers",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "InTraining",
                table: "Leads");

            migrationBuilder.DropColumn(
                name: "AdditionalData",
                table: "Column");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Answers",
                newName: "QuestionId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Answers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Answers",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Answers",
                table: "Answers",
                columns: new[] { "UserId", "QuestionId" });
        }
    }
}
