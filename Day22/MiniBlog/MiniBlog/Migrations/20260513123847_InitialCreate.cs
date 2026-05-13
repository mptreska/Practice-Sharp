using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MiniBlog.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 5000, nullable: false),
                    Author = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Author", "Content", "CreatedAt", "Title" },
                values: new object[,]
                {
                    { 1, "Иван Иванов", "Это содержимое первого поста в нашем блоге", new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Первый пост" },
                    { 2, "Мария Петрова", "Это содержимое второго поста в нашем блоге", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Второй пост" },
                    { 3, "Алексей Сидоров", "Это содержимое третьего поста в нашем блоге", new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Третий пост" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
