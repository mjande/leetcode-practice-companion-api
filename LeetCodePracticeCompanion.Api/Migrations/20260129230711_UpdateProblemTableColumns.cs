using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeetCodePracticeCompanion.Api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProblemTableColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_done",
                table: "problems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_done",
                table: "problems",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }
    }
}
