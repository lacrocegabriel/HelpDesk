using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUsuarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioXCliente_Clientes_IdCliente",
                table: "UsuarioXCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioXCliente_Usuarios_IdUsuario",
                table: "UsuarioXCliente");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioXGerenciador_Gerenciadores_IdGerenciador",
                table: "UsuarioXGerenciador");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuarioXGerenciador_Usuarios_IdUsuario",
                table: "UsuarioXGerenciador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioXGerenciador",
                table: "UsuarioXGerenciador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuarioXCliente",
                table: "UsuarioXCliente");

            migrationBuilder.DropColumn(
                name: "Numero",
                table: "Chamados");

            migrationBuilder.RenameTable(
                name: "UsuarioXGerenciador",
                newName: "UsuariosXGerenciadores");

            migrationBuilder.RenameTable(
                name: "UsuarioXCliente",
                newName: "UsuariosXClientes");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioXGerenciador_IdUsuario",
                table: "UsuariosXGerenciadores",
                newName: "IX_UsuariosXGerenciadores_IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_UsuarioXCliente_IdUsuario",
                table: "UsuariosXClientes",
                newName: "IX_UsuariosXClientes_IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosXGerenciadores",
                table: "UsuariosXGerenciadores",
                columns: new[] { "IdGerenciador", "IdUsuario" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuariosXClientes",
                table: "UsuariosXClientes",
                columns: new[] { "IdCliente", "IdUsuario" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosXClientes_Clientes_IdCliente",
                table: "UsuariosXClientes",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosXClientes_Usuarios_IdUsuario",
                table: "UsuariosXClientes",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosXGerenciadores_Gerenciadores_IdGerenciador",
                table: "UsuariosXGerenciadores",
                column: "IdGerenciador",
                principalTable: "Gerenciadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuariosXGerenciadores_Usuarios_IdUsuario",
                table: "UsuariosXGerenciadores",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosXClientes_Clientes_IdCliente",
                table: "UsuariosXClientes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosXClientes_Usuarios_IdUsuario",
                table: "UsuariosXClientes");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosXGerenciadores_Gerenciadores_IdGerenciador",
                table: "UsuariosXGerenciadores");

            migrationBuilder.DropForeignKey(
                name: "FK_UsuariosXGerenciadores_Usuarios_IdUsuario",
                table: "UsuariosXGerenciadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosXGerenciadores",
                table: "UsuariosXGerenciadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UsuariosXClientes",
                table: "UsuariosXClientes");

            migrationBuilder.RenameTable(
                name: "UsuariosXGerenciadores",
                newName: "UsuarioXGerenciador");

            migrationBuilder.RenameTable(
                name: "UsuariosXClientes",
                newName: "UsuarioXCliente");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosXGerenciadores_IdUsuario",
                table: "UsuarioXGerenciador",
                newName: "IX_UsuarioXGerenciador_IdUsuario");

            migrationBuilder.RenameIndex(
                name: "IX_UsuariosXClientes_IdUsuario",
                table: "UsuarioXCliente",
                newName: "IX_UsuarioXCliente_IdUsuario");

            migrationBuilder.AddColumn<long>(
                name: "Numero",
                table: "Chamados",
                type: "BIGINT",
                nullable: false,
                defaultValue: 0L)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioXGerenciador",
                table: "UsuarioXGerenciador",
                columns: new[] { "IdGerenciador", "IdUsuario" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_UsuarioXCliente",
                table: "UsuarioXCliente",
                columns: new[] { "IdCliente", "IdUsuario" });

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioXCliente_Clientes_IdCliente",
                table: "UsuarioXCliente",
                column: "IdCliente",
                principalTable: "Clientes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioXCliente_Usuarios_IdUsuario",
                table: "UsuarioXCliente",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioXGerenciador_Gerenciadores_IdGerenciador",
                table: "UsuarioXGerenciador",
                column: "IdGerenciador",
                principalTable: "Gerenciadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UsuarioXGerenciador_Usuarios_IdUsuario",
                table: "UsuarioXGerenciador",
                column: "IdUsuario",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
