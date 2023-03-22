using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpDesk.Data.Migrations
{
    /// <inheritdoc />
    public partial class LoginUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Login",
                table: "Usuarios",
                type: "Varchar(14)",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "BIGINT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Login",
                table: "Usuarios",
                type: "BIGINT",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "Varchar(14)");
        }
    }
}
