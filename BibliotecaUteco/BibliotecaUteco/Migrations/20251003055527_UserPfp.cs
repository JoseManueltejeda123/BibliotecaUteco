using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaUteco.Migrations
{
    /// <inheritdoc />
    public partial class UserPfp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePictureUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 5, 55, 25, 908, DateTimeKind.Utc).AddTicks(6500), "Librarian", new DateTime(2025, 10, 3, 5, 55, 25, 908, DateTimeKind.Utc).AddTicks(6505) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 10, 3, 5, 55, 25, 908, DateTimeKind.Utc).AddTicks(7797), new DateTime(2025, 10, 3, 5, 55, 25, 908, DateTimeKind.Utc).AddTicks(7798) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "ProfilePictureUrl", "UpdatedAt", "Username" },
                values: new object[] { new DateTime(2025, 10, 3, 5, 55, 25, 919, DateTimeKind.Utc).AddTicks(1970), null, new DateTime(2025, 10, 3, 5, 55, 25, 919, DateTimeKind.Utc).AddTicks(1973), "juan.perez" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "ProfilePictureUrl", "UpdatedAt", "Username" },
                values: new object[] { new DateTime(2025, 10, 3, 5, 55, 25, 955, DateTimeKind.Utc).AddTicks(129), null, new DateTime(2025, 10, 3, 5, 55, 25, 955, DateTimeKind.Utc).AddTicks(134), "jose.perez" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureUrl",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "Name", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 30, 5, 1, 18, 786, DateTimeKind.Utc).AddTicks(6276), "Bibliotecario", new DateTime(2025, 9, 30, 5, 1, 18, 786, DateTimeKind.Utc).AddTicks(6281) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt" },
                values: new object[] { new DateTime(2025, 9, 30, 5, 1, 18, 786, DateTimeKind.Utc).AddTicks(8447), new DateTime(2025, 9, 30, 5, 1, 18, 786, DateTimeKind.Utc).AddTicks(8451) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "UpdatedAt", "Username" },
                values: new object[] { new DateTime(2025, 9, 30, 5, 1, 18, 798, DateTimeKind.Utc).AddTicks(7095), new DateTime(2025, 9, 30, 5, 1, 18, 798, DateTimeKind.Utc).AddTicks(7100), null });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "UpdatedAt", "Username" },
                values: new object[] { new DateTime(2025, 9, 30, 5, 1, 18, 844, DateTimeKind.Utc).AddTicks(6039), new DateTime(2025, 9, 30, 5, 1, 18, 844, DateTimeKind.Utc).AddTicks(6044), null });
        }
    }
}
