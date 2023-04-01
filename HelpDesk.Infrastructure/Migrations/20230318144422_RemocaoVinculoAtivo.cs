using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemocaoVinculoAtivo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VinculoAtivo",
                table: "UsuariosXGerenciadores");

            migrationBuilder.DropColumn(
                name: "VinculoAtivo",
                table: "UsuariosXClientes");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Usuarios");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "VinculoAtivo",
                table: "UsuariosXGerenciadores",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<bool>(
                name: "VinculoAtivo",
                table: "UsuariosXClientes",
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
    }
}
