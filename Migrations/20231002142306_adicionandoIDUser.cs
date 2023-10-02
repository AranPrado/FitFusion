using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitFusion.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoIDUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AspNetUserID",
                table: "Usuarios",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AspNetUserID",
                table: "Usuarios");
        }
    }
}
