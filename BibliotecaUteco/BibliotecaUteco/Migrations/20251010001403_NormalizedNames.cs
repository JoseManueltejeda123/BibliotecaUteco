using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaUteco.Migrations
{
    /// <inheritdoc />
    public partial class NormalizedNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Books",
                newName: "Sinopsis");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "Books",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(3740), new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(3745) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4905), new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4906) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4909), new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4910) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4911), new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4913) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4914), new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4915) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4917), new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4917) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4919), new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4920) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4921), new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4922) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4924), new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4924) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4926), new DateTime(2025, 10, 10, 0, 14, 1, 396, DateTimeKind.Utc).AddTicks(4927) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 328, DateTimeKind.Utc).AddTicks(4870), new DateTime(2025, 10, 10, 0, 14, 1, 328, DateTimeKind.Utc).AddTicks(4874) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 328, DateTimeKind.Utc).AddTicks(6802), new DateTime(2025, 10, 10, 0, 14, 1, 328, DateTimeKind.Utc).AddTicks(6804) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 341, DateTimeKind.Utc).AddTicks(1268), new DateTime(2025, 10, 10, 0, 14, 1, 341, DateTimeKind.Utc).AddTicks(1272) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 10, 0, 14, 1, 378, DateTimeKind.Utc).AddTicks(517), new DateTime(2025, 10, 10, 0, 14, 1, 378, DateTimeKind.Utc).AddTicks(526) });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_NormalizedName",
                table: "Genres",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Books_NormalizedName",
                table: "Books",
                column: "NormalizedName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Genres_NormalizedName",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Books_NormalizedName",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "Books");

            migrationBuilder.RenameColumn(
                name: "Sinopsis",
                table: "Books",
                newName: "Description");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Books",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(5673), new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(5678) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7221), new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7223) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7226), new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7227) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7229), new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7230) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7232), new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7232) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7235), new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7235) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7237), new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7238) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7240), new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7241) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7246), new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7247) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7251), new DateTime(2025, 10, 9, 4, 56, 44, 264, DateTimeKind.Utc).AddTicks(7253) });

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
    }
}
