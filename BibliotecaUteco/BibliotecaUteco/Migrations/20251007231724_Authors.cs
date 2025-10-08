using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaUteco.Migrations
{
    /// <inheritdoc />
    public partial class Authors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedFullName",
                table: "Authors",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

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

            migrationBuilder.CreateIndex(
                name: "IX_Authors_NormalizedFullName",
                table: "Authors",
                column: "NormalizedFullName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Authors_NormalizedFullName",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "NormalizedFullName",
                table: "Authors");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 5, 23, 22, 27, 415, DateTimeKind.Utc).AddTicks(702), new DateTime(2025, 10, 5, 23, 22, 27, 415, DateTimeKind.Utc).AddTicks(711) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 5, 23, 22, 27, 415, DateTimeKind.Utc).AddTicks(1914), new DateTime(2025, 10, 5, 23, 22, 27, 415, DateTimeKind.Utc).AddTicks(1916) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 5, 23, 22, 27, 424, DateTimeKind.Utc).AddTicks(7617), new DateTime(2025, 10, 5, 23, 22, 27, 424, DateTimeKind.Utc).AddTicks(7625) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 5, 23, 22, 27, 468, DateTimeKind.Utc).AddTicks(4374), new DateTime(2025, 10, 5, 23, 22, 27, 468, DateTimeKind.Utc).AddTicks(4380) });
        }
    }
}
