using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabideSolidario.Migrations
{
    /// <inheritdoc />
    public partial class AddSolicitacoesDoacoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAD_SOLICITACAO_DOACAO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDoador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Tamanhos = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Condicao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoPeca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisponivelParaEntrega = table.Column<bool>(type: "bit", nullable: false),
                    Instituicao = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAD_SOLICITACAO_DOACAO", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAD_SOLICITACAO_DOACAO");
        }
    }
}
