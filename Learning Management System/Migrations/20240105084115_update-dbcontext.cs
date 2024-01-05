using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Learning_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class updatedbcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ImageCover",
                table: "Courses",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CategoryImageCover",
                table: "CategoryCourses",
                type: "longtext",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "CategoryCourses",
                keyColumn: "CategoryId",
                keyValue: 1,
                column: "CategoryImageCover",
                value: null);

            migrationBuilder.UpdateData(
                table: "CategoryCourses",
                keyColumn: "CategoryId",
                keyValue: 2,
                column: "CategoryImageCover",
                value: null);

            migrationBuilder.UpdateData(
                table: "CategoryCourses",
                keyColumn: "CategoryId",
                keyValue: 3,
                column: "CategoryImageCover",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryImageCover",
                table: "CategoryCourses");

            migrationBuilder.AlterColumn<decimal>(
                name: "ImageCover",
                table: "Courses",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }
    }
}
