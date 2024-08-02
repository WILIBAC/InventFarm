using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Farma.Migrations
{
    /// <inheritdoc />
    public partial class ChangeModelMedicamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Saldo",
                table: "Medicamentos",
                newName: "Cantidad");

            migrationBuilder.RenameColumn(
                name: "Nombre",
                table: "Medicamentos",
                newName: "Producto");

            migrationBuilder.RenameColumn(
                name: "Descripcion",
                table: "Medicamentos",
                newName: "FormaFarma");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Producto",
                table: "Medicamentos",
                newName: "Nombre");

            migrationBuilder.RenameColumn(
                name: "FormaFarma",
                table: "Medicamentos",
                newName: "Descripcion");

            migrationBuilder.RenameColumn(
                name: "Cantidad",
                table: "Medicamentos",
                newName: "Saldo");
        }
    }
}
