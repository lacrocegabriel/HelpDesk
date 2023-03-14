using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class VinculoUsuarioAtivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "VinculoAtivo",
                table: "UsuarioXGerenciador",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "VinculoAtivo",
                table: "UsuarioXCliente",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VinculoAtivo",
                table: "UsuarioXGerenciador");

            migrationBuilder.DropColumn(
                name: "VinculoAtivo",
                table: "UsuarioXCliente");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Usuarios");
        }
    }
}
