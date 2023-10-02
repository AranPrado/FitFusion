using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitFusion.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoRolesUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioModelUserID",
                table: "AspNetUserRoles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UsuarioModelUserID",
                table: "AspNetUserRoles",
                column: "UsuarioModelUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_Usuarios_UsuarioModelUserID",
                table: "AspNetUserRoles",
                column: "UsuarioModelUserID",
                principalTable: "Usuarios",
                principalColumn: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_Usuarios_UsuarioModelUserID",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UsuarioModelUserID",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UsuarioModelUserID",
                table: "AspNetUserRoles");
        }
    }
}
