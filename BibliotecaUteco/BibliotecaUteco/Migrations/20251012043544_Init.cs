using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BibliotecaUteco.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombreCompletoNormalizado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    NombreNormalizado = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lectores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NumeroDeTelefono = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    Matricula = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: true),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lectores", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    NombreNormalizado = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UrlPortada = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Synopsis = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Copias = table.Column<int>(type: "int", nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxDiasDePrestamo = table.Column<int>(type: "int", nullable: false),
                    DiaDeEntrega = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiaDevuelto = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IdLector = table.Column<int>(type: "int", nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Prestamos_Lectores_IdLector",
                        column: x => x.IdLector,
                        principalTable: "Lectores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GeneroLibro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdGenero = table.Column<int>(type: "int", nullable: false),
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneroLibro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GeneroLibro_Genero_IdGenero",
                        column: x => x.IdGenero,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GeneroLibro_Libros_IdLibro",
                        column: x => x.IdLibro,
                        principalTable: "Libros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LibroAutor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAutor = table.Column<int>(type: "int", nullable: false),
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroAutor", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibroAutor_Autores_IdAutor",
                        column: x => x.IdAutor,
                        principalTable: "Autores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibroAutor_Libros_IdLibro",
                        column: x => x.IdLibro,
                        principalTable: "Libros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreCompleto = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Clave = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrlFotoDePerfil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NombreDeUsuario = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    IdRole = table.Column<int>(type: "int", nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LibroPrestamo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdLibro = table.Column<int>(type: "int", nullable: false),
                    IdPrestamos = table.Column<int>(type: "int", nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibroPrestamo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibroPrestamo_Libros_IdLibro",
                        column: x => x.IdLibro,
                        principalTable: "Libros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LibroPrestamo_Prestamos_IdPrestamos",
                        column: x => x.IdPrestamos,
                        principalTable: "Prestamos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TransaccionesCaja",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Monto = table.Column<double>(type: "float", nullable: false),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransaccionesCaja", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransaccionesCaja_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Penalizaciones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiasExcedidos = table.Column<int>(type: "int", nullable: false),
                    IdPrestamo = table.Column<int>(type: "int", nullable: false),
                    EsDebida = table.Column<bool>(type: "bit", nullable: false),
                    TazaDeMultaPorDia = table.Column<double>(type: "float", nullable: false),
                    TotalAPagar = table.Column<double>(type: "float", nullable: false),
                    MontoDevuelto = table.Column<double>(type: "float", nullable: false),
                    MontoDado = table.Column<double>(type: "float", nullable: false),
                    IdTransaccion = table.Column<int>(type: "int", nullable: false),
                    CreadoEn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ActualizadoEn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalizaciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalizaciones_Prestamos_IdPrestamo",
                        column: x => x.IdPrestamo,
                        principalTable: "Prestamos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Penalizaciones_TransaccionesCaja_IdTransaccion",
                        column: x => x.IdTransaccion,
                        principalTable: "TransaccionesCaja",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Genero",
                columns: new[] { "Id", "CreadoEn", "Nombre", "NombreNormalizado", "ActualizadoEn" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(5162), "Fantasía", "fantasia", new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(5170) },
                    { 2, new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7002), "Terror", "terror", new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7004) },
                    { 3, new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7008), "Ciencia Ficción", "cienciaficcion", new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7008) },
                    { 4, new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7011), "Romance", "romance", new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7011) },
                    { 5, new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7014), "Misterio", "misterio", new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7014) },
                    { 6, new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7016), "Aventura", "aventura", new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7017) },
                    { 7, new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7019), "Histórico", "historico", new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7020) },
                    { 8, new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7022), "Biografía", "biografia", new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7023) },
                    { 9, new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7025), "Poesía", "poesia", new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7025) },
                    { 10, new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7028), "Drama", "drama", new DateTime(2025, 10, 12, 4, 35, 42, 189, DateTimeKind.Utc).AddTicks(7028) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreadoEn", "Nombre", "ActualizadoEn" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 12, 4, 35, 42, 64, DateTimeKind.Utc).AddTicks(7244), "Librarian", new DateTime(2025, 10, 12, 4, 35, 42, 64, DateTimeKind.Utc).AddTicks(7251) },
                    { 2, new DateTime(2025, 10, 12, 4, 35, 42, 64, DateTimeKind.Utc).AddTicks(9205), "Admin", new DateTime(2025, 10, 12, 4, 35, 42, 64, DateTimeKind.Utc).AddTicks(9217) }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "CreadoEn", "NombreCompleto", "Cedula", "Clave", "UrlFotoDePerfil", "IdRole", "ActualizadoEn", "NombreDeUsuario" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 10, 12, 4, 35, 42, 79, DateTimeKind.Utc).AddTicks(8624), "José Apolinar", "00112345678", "973279fd3528bf897629f68765425a6b3e88e35b010c3c3c10a169283a817289", null, 1, new DateTime(2025, 10, 12, 4, 35, 42, 79, DateTimeKind.Utc).AddTicks(8630), "jose.apolinar" },
                    { 2, new DateTime(2025, 10, 12, 4, 35, 42, 144, DateTimeKind.Utc).AddTicks(8516), "Manuel López", "00212345678", "2540fc2a209dd5946b09734722f16821b435db8e376655ab334379a4a0de1133", null, 2, new DateTime(2025, 10, 12, 4, 35, 42, 144, DateTimeKind.Utc).AddTicks(8521), "manuel.lopez" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Autores_NombreCompletoNormalizado",
                table: "Autores",
                column: "NombreCompletoNormalizado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genero_NombreNormalizado",
                table: "Genero",
                column: "NombreNormalizado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneroLibro_IdGenero_IdLibro",
                table: "GeneroLibro",
                columns: new[] { "IdGenero", "IdLibro" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GeneroLibro_IdLibro",
                table: "GeneroLibro",
                column: "IdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_Lectores_Cedula",
                table: "Lectores",
                column: "Cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lectores_Matricula",
                table: "Lectores",
                column: "Matricula",
                unique: true,
                filter: "[Matricula] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_LibroAutor_IdAutor_IdLibro",
                table: "LibroAutor",
                columns: new[] { "IdAutor", "IdLibro" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LibroAutor_IdLibro",
                table: "LibroAutor",
                column: "IdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_LibroPrestamo_IdLibro",
                table: "LibroPrestamo",
                column: "IdLibro");

            migrationBuilder.CreateIndex(
                name: "IX_LibroPrestamo_IdPrestamos_IdLibro",
                table: "LibroPrestamo",
                columns: new[] { "IdPrestamos", "IdLibro" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Libros_NombreNormalizado",
                table: "Libros",
                column: "NombreNormalizado",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Penalizaciones_IdPrestamo",
                table: "Penalizaciones",
                column: "IdPrestamo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Penalizaciones_IdTransaccion",
                table: "Penalizaciones",
                column: "IdTransaccion",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_IdLector",
                table: "Prestamos",
                column: "IdLector");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_Nombre",
                table: "Roles",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransaccionesCaja_IdUsuario",
                table: "TransaccionesCaja",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Cedula",
                table: "Usuarios",
                column: "Cedula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdRole",
                table: "Usuarios",
                column: "IdRole");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_NombreDeUsuario",
                table: "Usuarios",
                column: "NombreDeUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GeneroLibro");

            migrationBuilder.DropTable(
                name: "LibroAutor");

            migrationBuilder.DropTable(
                name: "LibroPrestamo");

            migrationBuilder.DropTable(
                name: "Penalizaciones");

            migrationBuilder.DropTable(
                name: "Genero");

            migrationBuilder.DropTable(
                name: "Autores");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "TransaccionesCaja");

            migrationBuilder.DropTable(
                name: "Lectores");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
