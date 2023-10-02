using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitFusion.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoUsuarioModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_CargoModel_CargoID",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "ConfirmarSenha",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "CargoID",
                table: "Usuarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_CargoModel_CargoID",
                table: "Usuarios",
                column: "CargoID",
                principalTable: "CargoModel",
                principalColumn: "CargoID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_CargoModel_CargoID",
                table: "Usuarios");

            migrationBuilder.AlterColumn<int>(
                name: "CargoID",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConfirmarSenha",
                table: "Usuarios",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "Usuarios",
                type: "varchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_CargoModel_CargoID",
                table: "Usuarios",
                column: "CargoID",
                principalTable: "CargoModel",
                principalColumn: "CargoID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
