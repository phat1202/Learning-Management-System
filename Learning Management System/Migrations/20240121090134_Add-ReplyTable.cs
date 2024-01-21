using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Learning_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class AddReplyTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RepliesComments",
                columns: table => new
                {
                    ReplyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CommentReply = table.Column<string>(type: "longtext", nullable: true),
                    CommentId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true),
                    CommentedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepliesComments", x => x.ReplyId);
                    table.ForeignKey(
                        name: "FK_RepliesComments_CommentLessons_CommentId",
                        column: x => x.CommentId,
                        principalTable: "CommentLessons",
                        principalColumn: "CommentId");
                    table.ForeignKey(
                        name: "FK_RepliesComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_RepliesComments_CommentId",
                table: "RepliesComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_RepliesComments_UserId",
                table: "RepliesComments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepliesComments");
        }
    }
}
