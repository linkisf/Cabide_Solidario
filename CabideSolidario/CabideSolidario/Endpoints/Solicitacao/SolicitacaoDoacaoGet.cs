using CabideSolidario.Infra.Data;
using CabideSolidario.Domain.SolicitacaoDoacao;

namespace CabideSolidario.Endpoints.Solicitacao;

public class SolicitacaoDoacaoGet
{
    public static string Template => "/solicitacaodoacao";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(ApplicationDbContext context)
    {
        var solicitacoes = context.SolicitacaoDoacoes.ToList();

        return Results.Ok(solicitacoes);
    }
}
