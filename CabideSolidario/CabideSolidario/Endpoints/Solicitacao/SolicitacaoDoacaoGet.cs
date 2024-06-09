using CabideSolidario.Infra.Data;
using CabideSolidario.Domain.SolicitacaoDoacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CabideSolidario.Endpoints.Solicitacao;

public class SolicitacaoDoacaoGet
{
    public static string Template => "/solicitacaodoacao/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    [Authorize(Policy = "DoadorPolicy")]
    public static IResult Action([FromRoute] Guid id, ApplicationDbContext context)
    {
        var solicitacao = context.SolicitacaoDoacoes.Where(s => s.Id == id).ToList();

        if (solicitacao == null)
            return Results.BadRequest();

        return Results.Ok(solicitacao);
    }
}
