using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Votica.EntityFrameworkCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "participants",
                columns: table => new
                {
                    email = table.Column<string>(type: "varchar(60)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_participants", x => x.email);
                });

            migrationBuilder.CreateTable(
                name: "polls",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    description = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true),
                    creationDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false),
                    expirationDate = table.Column<DateTimeOffset>(type: "timestamptz", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_polls", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "questionTypes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "varchar(35)", maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "questions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false),
                    TypeId = table.Column<int>(nullable: false),
                    PollId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questions", x => x.id);
                    table.ForeignKey(
                        name: "FK_questions_polls_PollId",
                        column: x => x.PollId,
                        principalTable: "polls",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_questions_questionTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "questionTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "options",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_options", x => x.id);
                    table.ForeignKey(
                        name: "FK_options_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "optionsPerParticipant",
                columns: table => new
                {
                    ParticipantEmail = table.Column<string>(nullable: false),
                    OptionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_optionsPerParticipant", x => new { x.ParticipantEmail, x.OptionId });
                    table.ForeignKey(
                        name: "FK_optionsPerParticipant_options_OptionId",
                        column: x => x.OptionId,
                        principalTable: "options",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_optionsPerParticipant_participants_ParticipantEmail",
                        column: x => x.ParticipantEmail,
                        principalTable: "participants",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_options_QuestionId",
                table: "options",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_optionsPerParticipant_OptionId",
                table: "optionsPerParticipant",
                column: "OptionId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_PollId",
                table: "questions",
                column: "PollId");

            migrationBuilder.CreateIndex(
                name: "IX_questions_TypeId",
                table: "questions",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "optionsPerParticipant");

            migrationBuilder.DropTable(
                name: "options");

            migrationBuilder.DropTable(
                name: "participants");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "polls");

            migrationBuilder.DropTable(
                name: "questionTypes");
        }
    }
}
