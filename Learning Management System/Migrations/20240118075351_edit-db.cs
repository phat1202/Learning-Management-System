﻿using System;
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
            migrationBuilder.AddColumn<DateTime>(
                name: "CommentedAt",
                table: "Ratings",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CommentedAt",
                table: "CommentLessons",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentedAt",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "CommentedAt",
                table: "CommentLessons");
        }
    }
}
