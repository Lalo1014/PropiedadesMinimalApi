using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropiedadesMinimalApi.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablaPropiedad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Propiedad",
                columns: table => new
                {
                    IdPropiedad = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ubicacion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Activa = table.Column<bool>(type: "bit", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propiedad", x => x.IdPropiedad);
                });

            migrationBuilder.InsertData(
                table: "Propiedad",
                columns: new[] { "IdPropiedad", "Activa", "Descripcion", "FechaCreacion", "Nombre", "Ubicacion" },
                values: new object[,]
                {
                    { 1, true, "casa desc 1", new DateTime(2025, 11, 16, 16, 37, 0, 416, DateTimeKind.Local).AddTicks(263), "casa 1", "mexico" },
                    { 2, true, "casa desc 2", new DateTime(2025, 11, 16, 16, 37, 0, 416, DateTimeKind.Local).AddTicks(312), "casa 2", "mexico" },
                    { 3, true, "casa desc 3", new DateTime(2025, 11, 16, 16, 37, 0, 416, DateTimeKind.Local).AddTicks(318), "casa 3", "mexico" },
                    { 4, true, "casa desc 4", new DateTime(2025, 11, 16, 16, 37, 0, 416, DateTimeKind.Local).AddTicks(323), "casa 4", "mexico" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propiedad");
        }
    }
}
