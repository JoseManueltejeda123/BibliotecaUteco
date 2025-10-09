using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BibliotecaUteco.Migrations
{
    /// <inheritdoc />
    public partial class Genres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "Genres",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "CreatedAt", "Name", "NormalizedName", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(5673), "Fantasía", "fantasia", new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(5678) },
                    { 2, new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7221), "Terror", "terror", new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7223) },
                    { 3, new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7226), "Ciencia Ficción", "ciencia ficcion", new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7227) },
                    { 4, new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7229), "Romance", "romance", new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7230) },
                    { 5, new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7232), "Misterio", "misterio", new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7232) },
                    { 6, new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7235), "Aventura", "aventura", new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7235) },
                    { 7, new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7237), "Histórico", "historico", new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7238) },
                    { 8, new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7240), "Biografía", "biografia", new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7241) },
                    { 9, new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7246), "Poesía", "poesia", new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7247) },
                    { 10, new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7251), "Drama", "drama", new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7253) }
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 178, DateTimeKind.Utc).AddTicks(5274), new DateTime(2025, 10, 9, 4, 56, 44, 178, DateTimeKind.Utc).AddTicks(5279) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 178, DateTimeKind.Utc).AddTicks(6709), new DateTime(2025, 10, 9, 4, 56, 44, 178, DateTimeKind.Utc).AddTicks(6712) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 192, DateTimeKind.Utc).AddTicks(6749), new DateTime(2025, 10, 9, 4, 56, 44, 192, DateTimeKind.Utc).AddTicks(6754) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 241, DateTimeKind.Utc).AddTicks(176), new DateTime(2025, 10, 9, 4, 56, 44, 241, DateTimeKind.Utc).AddTicks(180) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "Genres");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 7, 23, 17, 22, 202, DateTimeKind.Utc).AddTicks(4022), new DateTime(2025, 10, 7, 23, 17, 22, 202, DateTimeKind.Utc).AddTicks(4026) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 7, 23, 17, 22, 202, DateTimeKind.Utc).AddTicks(5005), new DateTime(2025, 10, 7, 23, 17, 22, 202, DateTimeKind.Utc).AddTicks(5006) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 7, 23, 17, 22, 214, DateTimeKind.Utc).AddTicks(5676), new DateTime(2025, 10, 7, 23, 17, 22, 214, DateTimeKind.Utc).AddTicks(5681) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 7, 23, 17, 22, 258, DateTimeKind.Utc).AddTicks(4491), new DateTime(2025, 10, 7, 23, 17, 22, 258, DateTimeKind.Utc).AddTicks(4496) });
        }
    }
}
