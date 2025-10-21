using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaUteco.Migrations
{
    /// <inheritdoc />
    public partial class maxloandays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(163), new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(166) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1320), new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1322) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1687), new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1689) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1692), new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1693) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1695), new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1695) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1697), new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1697) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1699), new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1700) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1701), new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1702) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1704), new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1704) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1706), new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1706) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 146, DateTimeKind.Utc).AddTicks(775), new DateTime(2025, 10, 20, 20, 16, 20, 146, DateTimeKind.Utc).AddTicks(780) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 146, DateTimeKind.Utc).AddTicks(2261), new DateTime(2025, 10, 20, 20, 16, 20, 146, DateTimeKind.Utc).AddTicks(2262) });

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 149, DateTimeKind.Utc).AddTicks(3499), new DateTime(2025, 10, 20, 20, 16, 20, 149, DateTimeKind.Utc).AddTicks(3503) });

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 149, DateTimeKind.Utc).AddTicks(4403), new DateTime(2025, 10, 20, 20, 16, 20, 149, DateTimeKind.Utc).AddTicks(4406) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 159, DateTimeKind.Utc).AddTicks(865), new DateTime(2025, 10, 20, 20, 16, 20, 159, DateTimeKind.Utc).AddTicks(869) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 20, 20, 16, 20, 201, DateTimeKind.Utc).AddTicks(1734), new DateTime(2025, 10, 20, 20, 16, 20, 201, DateTimeKind.Utc).AddTicks(1738) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(2793), new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(2799) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4472), new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4473) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4478), new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4478) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4480), new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4481) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4484), new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4484) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4487), new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4487) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4489), new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4490) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4492), new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4492) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4494), new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4495) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4497), new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4498) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 410, DateTimeKind.Utc).AddTicks(972), new DateTime(2025, 10, 18, 20, 49, 38, 410, DateTimeKind.Utc).AddTicks(978) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 410, DateTimeKind.Utc).AddTicks(2567), new DateTime(2025, 10, 18, 20, 49, 38, 410, DateTimeKind.Utc).AddTicks(2571) });

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 416, DateTimeKind.Utc).AddTicks(2263), new DateTime(2025, 10, 18, 20, 49, 38, 416, DateTimeKind.Utc).AddTicks(2267) });

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 416, DateTimeKind.Utc).AddTicks(3178), new DateTime(2025, 10, 18, 20, 49, 38, 416, DateTimeKind.Utc).AddTicks(3179) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 445, DateTimeKind.Utc).AddTicks(4442), new DateTime(2025, 10, 18, 20, 49, 38, 445, DateTimeKind.Utc).AddTicks(4448) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 18, 20, 49, 38, 561, DateTimeKind.Utc).AddTicks(2213), new DateTime(2025, 10, 18, 20, 49, 38, 561, DateTimeKind.Utc).AddTicks(2223) });
        }
    }
}
