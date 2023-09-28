using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitFusion.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCargos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_CargoModel_CargoID",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CargoModel",
                table: "CargoModel");

            migrationBuilder.RenameTable(
                name: "CargoModel",
                newName: "Cargos");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cargos",
                table: "Cargos",
                column: "CargoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Cargos_CargoID",
                table: "Usuarios",
                column: "CargoID",
                principalTable: "Cargos",
                principalColumn: "CargoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cargos_CargoID",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cargos",
                table: "Cargos");

            migrationBuilder.RenameTable(
                name: "Cargos",
                newName: "CargoModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CargoModel",
                table: "CargoModel",
                column: "CargoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_CargoModel_CargoID",
                table: "Usuarios",
                column: "CargoID",
                principalTable: "CargoModel",
                principalColumn: "CargoID");
        }
    }
}
