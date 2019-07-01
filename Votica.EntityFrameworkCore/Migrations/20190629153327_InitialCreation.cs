using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Votica.EntityFrameworkCore.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "users",
                columns: table => new
                {
                    email = table.Column<string>(type: "varchar(60)", nullable: false),
                    name = table.Column<string>(type: "varchar(45)", nullable: false),
                    lastname = table.Column<string>(type: "varchar(90)", nullable: false),
                    salt = table.Column<string>(type: "varchar(16)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.email);
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
                name: "answers",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    QuestionId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answers", x => x.id);
                    table.ForeignKey(
                        name: "FK_answers_questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "questions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "answersPerUser",
                columns: table => new
                {
                    UserEmail = table.Column<string>(nullable: false),
                    AnswerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_answersPerUser", x => new { x.UserEmail, x.AnswerId });
                    table.ForeignKey(
                        name: "FK_answersPerUser_answers_AnswerId",
                        column: x => x.AnswerId,
                        principalTable: "answers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_answersPerUser_users_UserEmail",
                        column: x => x.UserEmail,
                        principalTable: "users",
                        principalColumn: "email",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_answers_QuestionId",
                table: "answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_answersPerUser_AnswerId",
                table: "answersPerUser",
                column: "AnswerId");

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
                name: "answersPerUser");

            migrationBuilder.DropTable(
                name: "answers");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "questions");

            migrationBuilder.DropTable(
                name: "polls");

            migrationBuilder.DropTable(
                name: "questionTypes");
        }
    }
}
