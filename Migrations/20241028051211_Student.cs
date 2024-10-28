using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMvc.Migrations
{
    /// <inheritdoc />
    public partial class Student : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    name = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    info = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    dept1_id = table.Column<string>(type: "NVARCHAR2(5)", nullable: true),
                    dept2_id = table.Column<string>(type: "NVARCHAR2(5)", nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student", x => x.id);
                    table.ForeignKey(
                        name: "FK_student_department_dept1_id",
                        column: x => x.dept1_id,
                        principalTable: "department",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_student_department_dept2_id",
                        column: x => x.dept2_id,
                        principalTable: "department",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_student_dept1_id",
                table: "student",
                column: "dept1_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_dept2_id",
                table: "student",
                column: "dept2_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "student");
        }
    }
}
