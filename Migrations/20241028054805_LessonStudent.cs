using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMvc.Migrations
{
    /// <inheritdoc />
    public partial class LessonStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "lesson_student",
                columns: table => new
                {
                    lesson_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    student_id = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    info = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_lesson_student", x => new { x.lesson_id, x.student_id });
                    table.ForeignKey(
                        name: "FK_lesson_student_lesson_lesson_id",
                        column: x => x.lesson_id,
                        principalTable: "lesson",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_lesson_student_student_student_id",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_lesson_student_student_id",
                table: "lesson_student",
                column: "student_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "lesson_student");
        }
    }
}
