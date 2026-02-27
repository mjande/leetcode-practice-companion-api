using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeetCodePracticeCompanion.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddIntervalDaysandIntervalMonthsColumnsToProblems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "current_interval",
                table: "problems",
                newName: "interval_months");

            migrationBuilder.AddColumn<int>(
                name: "interval_days",
                table: "problems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "interval_days",
                table: "problems");

            migrationBuilder.RenameColumn(
                name: "interval_months",
                table: "problems",
                newName: "current_interval");
        }
    }
}
