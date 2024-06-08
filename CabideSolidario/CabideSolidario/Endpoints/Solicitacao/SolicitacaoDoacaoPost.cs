using CabideSolidario.Infra.Data;
using CabideSolidario.Domain.SolicitacaoDoacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CabideSolidario.Endpoints.Security;

namespace CabideSolidario.Endpoints.Solicitacao;

public class SolicitacaoDoacaoPost
{
    public static string Template => "/solicitacaodoacao";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    [Authorize(Policy = "DoadorPolicy")]
    public static IResult Action(SolicitacaoDoacao solicitacao, HttpContext http, UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        //var user = userManager.FindByEmailAsync(loginRequest.Email).Result;
        //var userId = user.Id;

        context.SolicitacaoDoacoes.Add(solicitacao);
        context.SaveChanges();

        return Results.Created($"/solicitacaodoacao/{solicitacao.Id}", solicitacao.Id);
    }
}
