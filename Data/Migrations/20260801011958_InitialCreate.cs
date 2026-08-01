using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autores",
                columns: table => new
                {
                    autor_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nombre = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    nacionalidad = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autores", x => x.autor_id);
                });

            migrationBuilder.CreateTable(
                name: "Libros",
                columns: table => new
                {
                    libro_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    titulo = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    autor_id = table.Column<int>(type: "int", nullable: false),
                    ano_publicacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    genero = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libros", x => x.libro_id);
                    table.ForeignKey(
                        name: "FK_Libros_Autores_autor_id",
                        column: x => x.autor_id,
                        principalTable: "Autores",
                        principalColumn: "autor_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Prestamos",
                columns: table => new
                {
                    prestamo_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    libro_id = table.Column<int>(type: "int", nullable: false),
                    fecha_prestamo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fecha_devolucion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prestamos", x => x.prestamo_id);
                    table.ForeignKey(
                        name: "FK_Prestamos_Libros_libro_id",
                        column: x => x.libro_id,
                        principalTable: "Libros",
                        principalColumn: "libro_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Libros_ano_publicacion",
                table: "Libros",
                column: "ano_publicacion");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_autor_id",
                table: "Libros",
                column: "autor_id");

            migrationBuilder.CreateIndex(
                name: "IX_Libros_genero",
                table: "Libros",
                column: "genero");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_fecha_devolucion",
                table: "Prestamos",
                column: "fecha_devolucion");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_fecha_prestamo",
                table: "Prestamos",
                column: "fecha_prestamo");

            migrationBuilder.CreateIndex(
                name: "IX_Prestamos_libro_id",
                table: "Prestamos",
                column: "libro_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prestamos");

            migrationBuilder.DropTable(
                name: "Libros");

            migrationBuilder.DropTable(
                name: "Autores");
        }
    }
}
