using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CabideSolidario.Migrations
{
    /// <inheritdoc />
    public partial class CreateSolicitacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CAD_SOLICITACOES",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IdDoador = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    Condicao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TipoPeca = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DisponivelParaEntrega = table.Column<bool>(type: "bit", nullable: false),
                    Instituicao = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EditedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAD_SOLICITACOES", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CAD_SOLICITACOES");
        }
    }
}
