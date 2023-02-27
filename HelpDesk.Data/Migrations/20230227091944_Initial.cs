using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HelpDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Enderecos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(200)", nullable: false),
                    Numero = table.Column<string>(type: "varchar(50)", nullable: false),
                    Bairro = table.Column<string>(type: "varchar(100)", nullable: false),
                    Cidade = table.Column<string>(type: "varchar(100)", nullable: false),
                    Estado = table.Column<string>(type: "varchar(50)", nullable: false),
                    Complemento = table.Column<string>(type: "varchar(250)", nullable: true),
                    Uf = table.Column<string>(type: "varchar(2)", nullable: false),
                    Cep = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enderecos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TipoPessoa",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "Varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoPessoa", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "VARCHAR(200)", nullable: false),
                    Documento = table.Column<string>(type: "VARCHAR(14)", nullable: false),
                    Email = table.Column<string>(type: "Varchar(50)", nullable: false),
                    DataNascimentoConstituicao = table.Column<DateTime>(type: "DATE", nullable: false),
                    IdTipoPessoa = table.Column<long>(type: "bigint", nullable: false),
                    IdEndereco = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pessoas_Enderecos_IdEndereco",
                        column: x => x.IdEndereco,
                        principalTable: "Enderecos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Pessoas_TipoPessoa_IdTipoPessoa",
                        column: x => x.IdTipoPessoa,
                        principalTable: "TipoPessoa",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Gerenciadores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gerenciadores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gerenciadores_Pessoas_Id",
                        column: x => x.Id,
                        principalTable: "Pessoas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGerenciador = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clientes_Gerenciadores_IdGerenciador",
                        column: x => x.IdGerenciador,
                        principalTable: "Gerenciadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Clientes_Pessoas_Id",
                        column: x => x.Id,
                        principalTable: "Pessoas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdGerenciador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "VARCHAR(300)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setores_Gerenciadores_IdGerenciador",
                        column: x => x.IdGerenciador,
                        principalTable: "Gerenciadores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<long>(type: "BIGINT", nullable: false),
                    IdSetor = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Pessoas_Id",
                        column: x => x.Id,
                        principalTable: "Pessoas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Usuarios_Setores_IdSetor",
                        column: x => x.IdSetor,
                        principalTable: "Setores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Chamados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    IdGerenciador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuarioGerador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuarioResponsavel = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamados", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chamados_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chamados_Gerenciadores_IdGerenciador",
                        column: x => x.IdGerenciador,
                        principalTable: "Gerenciadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chamados_Usuarios_IdUsuarioGerador",
                        column: x => x.IdUsuarioGerador,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Chamados_Usuarios_IdUsuarioResponsavel",
                        column: x => x.IdUsuarioResponsavel,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioXCliente",
                columns: table => new
                {
                    IdCliente = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioXCliente", x => new { x.IdCliente, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK_UsuarioXCliente_Clientes_IdCliente",
                        column: x => x.IdCliente,
                        principalTable: "Clientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioXCliente_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UsuarioXGerenciador",
                columns: table => new
                {
                    IdGerenciador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdUsuario = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioXGerenciador", x => new { x.IdGerenciador, x.IdUsuario });
                    table.ForeignKey(
                        name: "FK_UsuarioXGerenciador_Gerenciadores_IdGerenciador",
                        column: x => x.IdGerenciador,
                        principalTable: "Gerenciadores",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UsuarioXGerenciador_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "TipoPessoa",
                columns: new[] { "Id", "Tipo" },
                values: new object[,]
                {
                    { 1L, "PessoaJuridica" },
                    { 2L, "PessoaFisica" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_IdCliente",
                table: "Chamados",
                column: "IdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_IdGerenciador",
                table: "Chamados",
                column: "IdGerenciador");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_IdUsuarioGerador",
                table: "Chamados",
                column: "IdUsuarioGerador");

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_IdUsuarioResponsavel",
                table: "Chamados",
                column: "IdUsuarioResponsavel");

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_IdGerenciador",
                table: "Clientes",
                column: "IdGerenciador");

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_IdEndereco",
                table: "Pessoas",
                column: "IdEndereco",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pessoas_IdTipoPessoa",
                table: "Pessoas",
                column: "IdTipoPessoa");

            migrationBuilder.CreateIndex(
                name: "IX_Setores_IdGerenciador",
                table: "Setores",
                column: "IdGerenciador");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdSetor",
                table: "Usuarios",
                column: "IdSetor");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioXCliente_IdUsuario",
                table: "UsuarioXCliente",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioXGerenciador_IdUsuario",
                table: "UsuarioXGerenciador",
                column: "IdUsuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Chamados");

            migrationBuilder.DropTable(
                name: "UsuarioXCliente");

            migrationBuilder.DropTable(
                name: "UsuarioXGerenciador");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Setores");

            migrationBuilder.DropTable(
                name: "Gerenciadores");

            migrationBuilder.DropTable(
                name: "Pessoas");

            migrationBuilder.DropTable(
                name: "Enderecos");

            migrationBuilder.DropTable(
                name: "TipoPessoa");
        }
    }
}
