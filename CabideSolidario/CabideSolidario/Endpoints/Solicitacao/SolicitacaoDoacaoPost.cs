using CabideSolidario.Infra.Data;
using CabideSolidario.Domain.SolicitacaoDoacao;

namespace CabideSolidario.Endpoints.Solicitacao;

public class SolicitacaoDoacaoPost
{
    public static string Template => "/solicitacaodoacao";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(SolicitacaoDoacaoRequest solicitacaoDoacaoRequest, ApplicationDbContext context)
    {
        var solicitacao = new SolicitacaoDoacao
        {
            IdDoador = solicitacaoDoacaoRequest.IdDoador,
            Quantidade = solicitacaoDoacaoRequest.Quantidade,
            Condicao = solicitacaoDoacaoRequest.Condicao,
            TipoPeca = solicitacaoDoacaoRequest.TipoPeca,
            DisponivelParaEntrega = solicitacaoDoacaoRequest.DisponivelParaEntrega,
            Status = solicitacaoDoacaoRequest.Status
        };

        context.SolicitacaoDoacoes.Add(solicitacao);
        context.SaveChanges();

        return Results.Created($"/solicitacaodoacao/{solicitacao.Id}", solicitacao.Id);
    }
}
