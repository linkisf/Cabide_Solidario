using CabideSolidario.Domain.SolicitacaoDoacoes;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Authorization;

namespace CabideSolidario.Endpoints.Solicitacao;

public class SolicitacaoDoacaoPutAlterarStatus
{
    public static string Template => "/solicitacaodoacao/alterarstatus";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handler => Action;

    [Authorize(Policy = "DoadorPolicy")]    
    public static IResult Action(SolicitacaoDoacaoAlterarStatusRequest solicitacaoRequest, ApplicationDbContext context)
    {
        var solicitacao = context.SolicitacaoDoacoes.Where(s => s.Id == solicitacaoRequest.idSolicitacao).FirstOrDefault();

        solicitacao.Status = solicitacaoRequest.Status;
        context.SaveChanges();

        return Results.Ok("Status alterado com sucesso.");

    }
}
