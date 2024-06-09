using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CabideSolidario.Endpoints.Solicitacao;

public class SolicitacaoDoacaoGetAll
{
    public static string Template => "/solicitacaodoacaoall/";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    [Authorize(Policy = "DoadorPolicy")]
    public static IResult Action(ApplicationDbContext context)
    {
        var solicitacoes = context.SolicitacaoDoacoes.ToList();

        if (solicitacoes == null)
            return Results.BadRequest();

        return Results.Ok(solicitacoes);
    }
}
