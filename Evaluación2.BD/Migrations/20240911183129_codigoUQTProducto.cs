using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoModelado2024.BD.Migrations
{
    /// <inheritdoc />
    public partial class codigoUQTProducto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "CodigoTProducto_UQ",
                table: "TProductos",
                column: "Codigo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "CodigoTProducto_UQ",
                table: "TProductos");
        }
    }
}
