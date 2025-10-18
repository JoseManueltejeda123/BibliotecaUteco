using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BibliotecaUteco.Migrations
{
    /// <inheritdoc />
    public partial class Sexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdSexo",
                table: "Usuarios",
                type: "int",
                nullable: true
            );

            migrationBuilder.AddColumn<int>(
                name: "IdSexo",
                table: "Lectores",
                type: "int",
                nullable: true
            );

            migrationBuilder.CreateTable(
                name: "Sexos",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(
                        type: "nvarchar(15)",
                        maxLength: 15,
                        nullable: false
                    ),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sexos", x => x.Id);
                }
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

            migrationBuilder.InsertData(
                table: "Sexos",
                columns: new[] { "Id", "CreadoEn", "Nombre", "ActualizadoEn" },
                values: new object[,]
                {
                    {
                        1,
                        new DateTime(2025, 10, 18, 4, 39, 8, 8, DateTimeKind.Utc).AddTicks(1179),
                        "M",
                        new DateTime(2025, 10, 18, 4, 39, 8, 8, DateTimeKind.Utc).AddTicks(1184),
                    },
                    {
                        2,
                        new DateTime(2025, 10, 18, 4, 39, 8, 8, DateTimeKind.Utc).AddTicks(2419),
                        "F",
                        new DateTime(2025, 10, 18, 4, 39, 8, 8, DateTimeKind.Utc).AddTicks(2422),
                    },
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreadoEn", "IdSexo", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 23, DateTimeKind.Utc).AddTicks(7890),
                    1,
                    new DateTime(2025, 10, 18, 4, 39, 8, 23, DateTimeKind.Utc).AddTicks(7899),
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreadoEn", "IdSexo", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 18, 4, 39, 8, 80, DateTimeKind.Utc).AddTicks(6366),
                    1,
                    new DateTime(2025, 10, 18, 4, 39, 8, 80, DateTimeKind.Utc).AddTicks(6374),
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdSexo",
                table: "Usuarios",
                column: "IdSexo"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Lectores_IdSexo",
                table: "Lectores",
                column: "IdSexo"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Sexos_Nombre",
                table: "Sexos",
                column: "Nombre",
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Lectores_Sexos_IdSexo",
                table: "Lectores",
                column: "IdSexo",
                principalTable: "Sexos",
                principalColumn: "Id"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Sexos_IdSexo",
                table: "Usuarios",
                column: "IdSexo",
                principalTable: "Sexos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Lectores_Sexos_IdSexo", table: "Lectores");

            migrationBuilder.DropForeignKey(name: "FK_Usuarios_Sexos_IdSexo", table: "Usuarios");

            migrationBuilder.DropTable(name: "Sexos");

            migrationBuilder.DropIndex(name: "IX_Usuarios_IdSexo", table: "Usuarios");

            migrationBuilder.DropIndex(name: "IX_Lectores_IdSexo", table: "Lectores");

            migrationBuilder.DropColumn(name: "IdSexo", table: "Usuarios");

            migrationBuilder.DropColumn(name: "IdSexo", table: "Lectores");

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 997, DateTimeKind.Utc).AddTicks(9015),
                    new DateTime(2025, 10, 17, 4, 31, 3, 997, DateTimeKind.Utc).AddTicks(9020),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(689),
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(691),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(697),
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(697),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(700),
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(700),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(702),
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(703),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 6,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(705),
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(706),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 7,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(708),
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(708),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 8,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(710),
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(711),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 9,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(713),
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(714),
                }
            );

            migrationBuilder.UpdateData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 10,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(716),
                    new DateTime(2025, 10, 17, 4, 31, 3, 998, DateTimeKind.Utc).AddTicks(717),
                }
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 916, DateTimeKind.Utc).AddTicks(1194),
                    new DateTime(2025, 10, 17, 4, 31, 3, 916, DateTimeKind.Utc).AddTicks(1199),
                }
            );

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 916, DateTimeKind.Utc).AddTicks(2836),
                    new DateTime(2025, 10, 17, 4, 31, 3, 916, DateTimeKind.Utc).AddTicks(2837),
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 925, DateTimeKind.Utc).AddTicks(9196),
                    new DateTime(2025, 10, 17, 4, 31, 3, 925, DateTimeKind.Utc).AddTicks(9200),
                }
            );

            migrationBuilder.UpdateData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreadoEn", "ActualizadoEn" },
                values: new object[]
                {
                    new DateTime(2025, 10, 17, 4, 31, 3, 978, DateTimeKind.Utc).AddTicks(4849),
                    new DateTime(2025, 10, 17, 4, 31, 3, 978, DateTimeKind.Utc).AddTicks(4854),
                }
            );
        }
    }
}
