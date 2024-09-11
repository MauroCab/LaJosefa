using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Evaluacion2.BD.Migrations
{
    /// <inheritdoc />
    public partial class modelbuilder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_TProductos_TProductoId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Renglones_Pedidos_PedidoId",
                table: "Renglones");

            migrationBuilder.DropForeignKey(
                name: "FK_Renglones_Productos_ProductoId",
                table: "Renglones");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_TProductos_TProductoId",
                table: "Productos",
                column: "TProductoId",
                principalTable: "TProductos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Renglones_Pedidos_PedidoId",
                table: "Renglones",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Renglones_Productos_ProductoId",
                table: "Renglones",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Productos_TProductos_TProductoId",
                table: "Productos");

            migrationBuilder.DropForeignKey(
                name: "FK_Renglones_Pedidos_PedidoId",
                table: "Renglones");

            migrationBuilder.DropForeignKey(
                name: "FK_Renglones_Productos_ProductoId",
                table: "Renglones");

            migrationBuilder.AddForeignKey(
                name: "FK_Productos_TProductos_TProductoId",
                table: "Productos",
                column: "TProductoId",
                principalTable: "TProductos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Renglones_Pedidos_PedidoId",
                table: "Renglones",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Renglones_Productos_ProductoId",
                table: "Renglones",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
