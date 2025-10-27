using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaUteco.Migrations
{
    /// <inheritdoc />
    public partial class NombresActualizados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "Usuarios",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "Usuarios",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "TransaccionesCaja",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "TransaccionesCaja",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "Sexos",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "Sexos",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "Roles",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "Roles",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "DiaDevuelto",
                table: "Prestamos",
                newName: "FechaDevolucion"
            );

            migrationBuilder.RenameColumn(
                name: "DiaDeEntrega",
                table: "Prestamos",
                newName: "FechaEntrega"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "Prestamos",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "Prestamos",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "Penalizaciones",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "Penalizaciones",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "Libros",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "Libros",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "LibroPrestamo",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "LibroPrestamo",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "LibroAutor",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "LibroAutor",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "Lectores",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "Lectores",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "GeneroLibro",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "GeneroLibro",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "Genero",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "Genero",
                newName: "FechaActualizacion"
            );

            migrationBuilder.RenameColumn(
                name: "CreadoEn",
                table: "Autores",
                newName: "FechaCreacion"
            );

            migrationBuilder.RenameColumn(
                name: "ActualizadoEn",
                table: "Autores",
                newName: "FechaActualizacion"
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(2793),
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(2799),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4472),
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4473),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4478),
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4478),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4480),
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4481),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4484),
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4484),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4487),
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4487),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4489),
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4490),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4492),
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4492),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4494),
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4495),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4497),
                    new DateTime(2025, 10, 18, 20, 49, 38, 624, DateTimeKind.Utc).AddTicks(4498),
                }
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 410, DateTimeKind.Utc).AddTicks(972),
                    new DateTime(2025, 10, 18, 20, 49, 38, 410, DateTimeKind.Utc).AddTicks(978),
                }
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 410, DateTimeKind.Utc).AddTicks(2567),
                    new DateTime(2025, 10, 18, 20, 49, 38, 410, DateTimeKind.Utc).AddTicks(2571),
                }
            );

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 416, DateTimeKind.Utc).AddTicks(2263),
                    new DateTime(2025, 10, 18, 20, 49, 38, 416, DateTimeKind.Utc).AddTicks(2267),
                }
            );

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 416, DateTimeKind.Utc).AddTicks(3178),
                    new DateTime(2025, 10, 18, 20, 49, 38, 416, DateTimeKind.Utc).AddTicks(3179),
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 445, DateTimeKind.Utc).AddTicks(4442),
                    new DateTime(2025, 10, 18, 20, 49, 38, 445, DateTimeKind.Utc).AddTicks(4448),
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaCreacion", "FechaActualizacion" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 20, 49, 38, 561, DateTimeKind.Utc).AddTicks(2213),
                    new DateTime(2025, 10, 18, 20, 49, 38, 561, DateTimeKind.Utc).AddTicks(2223),
                }
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Usuarios",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "Usuarios",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "TransaccionesCaja",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "TransaccionesCaja",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Sexos",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "Sexos",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Roles",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "Roles",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaEntrega",
                table: "Prestamos",
                newName: "DiaDeEntrega"
            );

            migrationBuilder.RenameColumn(
                name: "FechaDevolucion",
                table: "Prestamos",
                newName: "DiaDevuelto"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Prestamos",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "Prestamos",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Penalizaciones",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "Penalizaciones",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Libros",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "Libros",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "LibroPrestamo",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "LibroPrestamo",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "LibroAutor",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "LibroAutor",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Lectores",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "Lectores",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "GeneroLibro",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "GeneroLibro",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Genero",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "Genero",
                newName: "ActualizadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaCreacion",
                table: "Autores",
                newName: "CreadoEn"
            );

            migrationBuilder.RenameColumn(
                name: "FechaActualizacion",
                table: "Autores",
                newName: "ActualizadoEn"
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(1130),
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(1134),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2733),
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2736),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2741),
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2741),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2744),
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2745),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2747),
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2748),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2750),
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2752),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2754),
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2755),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2757),
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2758),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2760),
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2761),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2764),
                    new DateTime(2025, 10, 18, 4, 39, 8, 105, DateTimeKind.Utc).AddTicks(2764),
                }
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 1, DateTimeKind.Utc).AddTicks(5237),
                    new DateTime(2025, 10, 18, 4, 39, 8, 1, DateTimeKind.Utc).AddTicks(5241),
                }
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 1, DateTimeKind.Utc).AddTicks(7322),
                    new DateTime(2025, 10, 18, 4, 39, 8, 1, DateTimeKind.Utc).AddTicks(7327),
                }
            );

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 8, DateTimeKind.Utc).AddTicks(1179),
                    new DateTime(2025, 10, 18, 4, 39, 8, 8, DateTimeKind.Utc).AddTicks(1184),
                }
            );

            migrationBuilder.UpdateData(
                table: "Sexos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 8, DateTimeKind.Utc).AddTicks(2419),
                    new DateTime(2025, 10, 18, 4, 39, 8, 8, DateTimeKind.Utc).AddTicks(2422),
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 23, DateTimeKind.Utc).AddTicks(7890),
                    new DateTime(2025, 10, 18, 4, 39, 8, 23, DateTimeKind.Utc).AddTicks(7899),
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 80, DateTimeKind.Utc).AddTicks(6366),
                    new DateTime(2025, 10, 18, 4, 39, 8, 80, DateTimeKind.Utc).AddTicks(6374),
                }
            );
        }
    }
}
