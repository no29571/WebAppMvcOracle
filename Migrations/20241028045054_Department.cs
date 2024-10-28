using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebAppMvc.Migrations
{
    /// <inheritdoc />
    public partial class Department : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "department",
                columns: table => new
                {
                    id = table.Column<string>(type: "NVARCHAR2(5)", maxLength: 5, nullable: false),
                    name = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: false),
                    info = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: true),
                    created_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    updated_at = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_department", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "department");
        }
    }
}
