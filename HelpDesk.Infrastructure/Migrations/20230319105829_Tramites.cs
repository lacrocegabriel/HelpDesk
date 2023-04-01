using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class Tramites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tramites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descricao = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    IdUsuarioGerador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdChamado = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tramites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tramites_Chamados_IdChamado",
                        column: x => x.IdChamado,
                        principalTable: "Chamados",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tramites_Usuarios_IdUsuarioGerador",
                        column: x => x.IdUsuarioGerador,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tramites_IdChamado",
                table: "Tramites",
                column: "IdChamado");

            migrationBuilder.CreateIndex(
                name: "IX_Tramites_IdUsuarioGerador",
                table: "Tramites",
                column: "IdUsuarioGerador");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tramites");
        }
    }
}
