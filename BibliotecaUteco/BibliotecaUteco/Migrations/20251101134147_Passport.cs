using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaUteco.Migrations
{
    /// <inheritdoc />
    public partial class Passport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pasaporte",
                table: "Lectores",
                type: "nvarchar(9)",
                maxLength: 9,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 438, DateTimeKind.Utc).AddTicks(9444), new DateTime(2025, 11, 1, 13, 41, 44, 438, DateTimeKind.Utc).AddTicks(9450) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1139), new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1141) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1144), new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1145) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1147), new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1148) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1151), new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1151) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1154), new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1154) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1157), new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1158) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1160), new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1161) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1164), new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1164) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1166), new DateTime(2025, 11, 1, 13, 41, 44, 439, DateTimeKind.Utc).AddTicks(1167) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 337, DateTimeKind.Utc).AddTicks(2011), new DateTime(2025, 11, 1, 13, 41, 44, 337, DateTimeKind.Utc).AddTicks(2029) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 337, DateTimeKind.Utc).AddTicks(3678), new DateTime(2025, 11, 1, 13, 41, 44, 337, DateTimeKind.Utc).AddTicks(3679) });

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 344, DateTimeKind.Utc).AddTicks(2104), new DateTime(2025, 11, 1, 13, 41, 44, 344, DateTimeKind.Utc).AddTicks(2109) });

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 344, DateTimeKind.Utc).AddTicks(3165), new DateTime(2025, 11, 1, 13, 41, 44, 344, DateTimeKind.Utc).AddTicks(3167) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 361, DateTimeKind.Utc).AddTicks(3999), new DateTime(2025, 11, 1, 13, 41, 44, 361, DateTimeKind.Utc).AddTicks(4003) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 11, 1, 13, 41, 44, 418, DateTimeKind.Utc).AddTicks(266), new DateTime(2025, 11, 1, 13, 41, 44, 418, DateTimeKind.Utc).AddTicks(269) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pasaporte",
                table: "Lectores");

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(5770), new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(5778) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7034), new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7035) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7038), new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7039) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7041), new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7042) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7043), new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7044) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7046), new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7046) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7048), new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7048) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7050), new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7051) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7052), new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7053) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7055), new DateTime(2025, 10, 27, 19, 22, 10, 735, DateTimeKind.Utc).AddTicks(7055) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 679, DateTimeKind.Utc).AddTicks(1565), new DateTime(2025, 10, 27, 19, 22, 10, 679, DateTimeKind.Utc).AddTicks(1571) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 679, DateTimeKind.Utc).AddTicks(2557), new DateTime(2025, 10, 27, 19, 22, 10, 679, DateTimeKind.Utc).AddTicks(2558) });

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 682, DateTimeKind.Utc).AddTicks(2556), new DateTime(2025, 10, 27, 19, 22, 10, 682, DateTimeKind.Utc).AddTicks(2563) });

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 682, DateTimeKind.Utc).AddTicks(3262), new DateTime(2025, 10, 27, 19, 22, 10, 682, DateTimeKind.Utc).AddTicks(3263) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 690, DateTimeKind.Utc).AddTicks(4025), new DateTime(2025, 10, 27, 19, 22, 10, 690, DateTimeKind.Utc).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 724, DateTimeKind.Utc).AddTicks(5536), new DateTime(2025, 10, 27, 19, 22, 10, 724, DateTimeKind.Utc).AddTicks(5542) });
        }
    }
}
