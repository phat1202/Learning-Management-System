using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class editdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Score",
                table: "StudentProgresses");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "ContentUrl",
                table: "Lessons",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "ItemSelected",
                table: "CartItems",
                type: "tinyint(1)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentUrl",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "ItemSelected",
                table: "CartItems");

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "StudentProgresses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "Lessons",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
