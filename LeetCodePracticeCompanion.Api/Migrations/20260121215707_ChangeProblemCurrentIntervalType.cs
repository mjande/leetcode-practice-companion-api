using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeetCodePracticeCompanion.Api.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProblemCurrentIntervalType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "current_interval",
                table: "problems",
                type: "TEXT",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "current_interval",
                table: "problems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 20,
                oldNullable: true);
        }
    }
}
