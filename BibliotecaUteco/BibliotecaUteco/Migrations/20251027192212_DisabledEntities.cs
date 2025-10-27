using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaUteco.Migrations
{
    /// <inheritdoc />
    public partial class DisabledEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDisabled",
                table: "Libros",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
                columns: new[] { "FechaCreacion", "IsDisabled", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 690, DateTimeKind.Utc).AddTicks(4025), false, new DateTime(2025, 10, 27, 19, 22, 10, 690, DateTimeKind.Utc).AddTicks(4030) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "IsDisabled", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 27, 19, 22, 10, 724, DateTimeKind.Utc).AddTicks(5536), false, new DateTime(2025, 10, 27, 19, 22, 10, 724, DateTimeKind.Utc).AddTicks(5542) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "IsDisabled",
                table: "Libros");

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(3126), new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(3129) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4781), new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4782) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4786), new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4786) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4788), new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4789) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4791), new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4792) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4794), new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4794) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4799), new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4800) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4804), new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4805) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4809), new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4812) });

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4814), new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4815) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 12, 985, DateTimeKind.Utc).AddTicks(910), new DateTime(2025, 10, 21, 1, 39, 12, 985, DateTimeKind.Utc).AddTicks(917) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 12, 985, DateTimeKind.Utc).AddTicks(2741), new DateTime(2025, 10, 21, 1, 39, 12, 985, DateTimeKind.Utc).AddTicks(2745) });

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 12, 995, DateTimeKind.Utc).AddTicks(2651), new DateTime(2025, 10, 21, 1, 39, 12, 995, DateTimeKind.Utc).AddTicks(2655) });

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 12, 995, DateTimeKind.Utc).AddTicks(3667), new DateTime(2025, 10, 21, 1, 39, 12, 995, DateTimeKind.Utc).AddTicks(3671) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 30, DateTimeKind.Utc).AddTicks(8540), new DateTime(2025, 10, 21, 1, 39, 13, 30, DateTimeKind.Utc).AddTicks(8544) });

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[] { new DateTime(2025, 10, 21, 1, 39, 13, 123, DateTimeKind.Utc).AddTicks(2163), new DateTime(2025, 10, 21, 1, 39, 13, 123, DateTimeKind.Utc).AddTicks(2167) });
        }
    }
}
