using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProyectoModelado2024.BD.Migrations
{
    /// <inheritdoc />
    public partial class EsComun : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EsComun",
                table: "Productos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EsComun",
                table: "Productos");
        }
    }
}
