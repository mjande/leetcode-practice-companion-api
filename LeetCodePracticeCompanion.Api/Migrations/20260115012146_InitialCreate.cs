using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeetCodePracticeCompanion.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "problems",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    number = table.Column<int>(type: "INTEGER", nullable: false),
                    name = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    difficulty = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    current_interval = table.Column<int>(type: "INTEGER", nullable: false),
                    last_solve_date = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    due_date = table.Column<DateOnly>(type: "TEXT", nullable: true),
                    source = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    url = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    is_done = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_problems", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "problems");
        }
    }
}
