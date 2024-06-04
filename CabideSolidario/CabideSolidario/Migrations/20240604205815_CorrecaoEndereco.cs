using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabideSolidario.Migrations
{
    /// <inheritdoc />
    public partial class CorrecaoEndereco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAD_ENDERECO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cep = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rua = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Complemento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<string>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAD_ENDERECO", x => x.Id);
                });


            migrationBuilder.DropPrimaryKey(
                name: "PK_CAD_SOLICITACOES",
                table: "CAD_SOLICITACOES");

            migrationBuilder.RenameTable(
                name: "CAD_SOLICITACOES",
                newName: "SolicitacaoDoacoes");            

            migrationBuilder.AddPrimaryKey(
                name: "PK_SolicitacaoDoacoes",
                table: "SolicitacaoDoacoes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SolicitacaoDoacoes",
                table: "SolicitacaoDoacoes");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "CAD_ENDERECO");

            migrationBuilder.RenameTable(
                name: "SolicitacaoDoacoes",
                newName: "CAD_SOLICITACOES");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CAD_SOLICITACOES",
                table: "CAD_SOLICITACOES",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CAD_USUARIOS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAD_USUARIOS", x => x.Id);
                });
        }
    }
}
