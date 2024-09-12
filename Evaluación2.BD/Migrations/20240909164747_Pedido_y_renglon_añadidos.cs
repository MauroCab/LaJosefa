using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoModelado2024.BD.Migrations
{
    /// <inheritdoc />
    public partial class Pedido_y_renglon_añadidos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Pedidos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pedidos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Renglones",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PedidoId = table.Column<int>(type: "int", nullable: false),
                    ProductoId = table.Column<int>(type: "int", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Renglones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Renglones_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalTable: "Pedidos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Renglones_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "TProducto_UQ",
                table: "TProductos",
                columns: new[] { "Codigo", "Nombre" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Producto_UQ",
                table: "Productos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Renglones_PedidoId",
                table: "Renglones",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Renglones_ProductoId",
                table: "Renglones",
                column: "ProductoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Renglones");

            migrationBuilder.DropTable(
                name: "Pedidos");

            migrationBuilder.DropIndex(
                name: "TProducto_UQ",
                table: "TProductos");

            migrationBuilder.DropIndex(
                name: "Producto_UQ",
                table: "Productos");
        }
    }
}
