using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HelpDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class SituacaoChamado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "IdSituacaoChamado",
                table: "Chamados",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "SituacaoChamado",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Situacao = table.Column<string>(type: "Varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SituacaoChamado", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SituacaoChamado",
                columns: new[] { "Id", "Situacao" },
                values: new object[,]
                {
                    { 1L, "Em Andamento" },
                    { 2L, "Aguardando" },
                    { 3L, "Fechado" },
                    { 4L, "Cancelado" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Chamados_IdSituacaoChamado",
                table: "Chamados",
                column: "IdSituacaoChamado");

            migrationBuilder.AddForeignKey(
                name: "FK_Chamados_SituacaoChamado_IdSituacaoChamado",
                table: "Chamados",
                column: "IdSituacaoChamado",
                principalTable: "SituacaoChamado",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chamados_SituacaoChamado_IdSituacaoChamado",
                table: "Chamados");

            migrationBuilder.DropTable(
                name: "SituacaoChamado");

            migrationBuilder.DropIndex(
                name: "IX_Chamados_IdSituacaoChamado",
                table: "Chamados");

            migrationBuilder.DropColumn(
                name: "IdSituacaoChamado",
                table: "Chamados");
        }
    }
}
