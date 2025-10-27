using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaUteco.Migrations
{
    /// <inheritdoc />
    public partial class PenalizationTransactions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Penalizaciones_IdTransaccion",
                table: "Penalizaciones"
            );

            migrationBuilder.AlterColumn<int>(
                name: "IdTransaccion",
                table: "Penalizaciones",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int"
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(3126),
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(3129),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4781),
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4782),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4786),
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4786),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4788),
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4789),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4791),
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4792),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4794),
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4794),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4799),
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4800),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4804),
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4805),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4809),
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4812),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4814),
                    new DateTime(2025, 10, 21, 1, 39, 13, 169, DateTimeKind.Utc).AddTicks(4815),
                }
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 12, 985, DateTimeKind.Utc).AddTicks(910),
                    new DateTime(2025, 10, 21, 1, 39, 12, 985, DateTimeKind.Utc).AddTicks(917),
                }
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 12, 985, DateTimeKind.Utc).AddTicks(2741),
                    new DateTime(2025, 10, 21, 1, 39, 12, 985, DateTimeKind.Utc).AddTicks(2745),
                }
            );

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 12, 995, DateTimeKind.Utc).AddTicks(2651),
                    new DateTime(2025, 10, 21, 1, 39, 12, 995, DateTimeKind.Utc).AddTicks(2655),
                }
            );

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 12, 995, DateTimeKind.Utc).AddTicks(3667),
                    new DateTime(2025, 10, 21, 1, 39, 12, 995, DateTimeKind.Utc).AddTicks(3671),
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 30, DateTimeKind.Utc).AddTicks(8540),
                    new DateTime(2025, 10, 21, 1, 39, 13, 30, DateTimeKind.Utc).AddTicks(8544),
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 21, 1, 39, 13, 123, DateTimeKind.Utc).AddTicks(2163),
                    new DateTime(2025, 10, 21, 1, 39, 13, 123, DateTimeKind.Utc).AddTicks(2167),
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Penalizaciones_IdTransaccion",
                table: "Penalizaciones",
                column: "IdTransaccion",
                unique: true,
                filter: "[IdTransaccion] IS NOT NULL"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Penalizaciones_IdTransaccion",
                table: "Penalizaciones"
            );

            migrationBuilder.AlterColumn<int>(
                name: "IdTransaccion",
                table: "Penalizaciones",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(163),
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(166),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1320),
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1322),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1687),
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1689),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1692),
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1693),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1695),
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1695),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1697),
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1697),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1699),
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1700),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1701),
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1702),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1704),
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1704),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1706),
                    new DateTime(2025, 10, 20, 20, 16, 20, 217, DateTimeKind.Utc).AddTicks(1706),
                }
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 146, DateTimeKind.Utc).AddTicks(775),
                    new DateTime(2025, 10, 20, 20, 16, 20, 146, DateTimeKind.Utc).AddTicks(780),
                }
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 146, DateTimeKind.Utc).AddTicks(2261),
                    new DateTime(2025, 10, 20, 20, 16, 20, 146, DateTimeKind.Utc).AddTicks(2262),
                }
            );

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 149, DateTimeKind.Utc).AddTicks(3499),
                    new DateTime(2025, 10, 20, 20, 16, 20, 149, DateTimeKind.Utc).AddTicks(3503),
                }
            );

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 149, DateTimeKind.Utc).AddTicks(4403),
                    new DateTime(2025, 10, 20, 20, 16, 20, 149, DateTimeKind.Utc).AddTicks(4406),
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 159, DateTimeKind.Utc).AddTicks(865),
                    new DateTime(2025, 10, 20, 20, 16, 20, 159, DateTimeKind.Utc).AddTicks(869),
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 20, 20, 16, 20, 201, DateTimeKind.Utc).AddTicks(1734),
                    new DateTime(2025, 10, 20, 20, 16, 20, 201, DateTimeKind.Utc).AddTicks(1738),
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Penalizaciones_IdTransaccion",
                table: "Penalizaciones",
                column: "IdTransaccion",
                unique: true
            );
        }
    }
}
