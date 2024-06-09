using CabideSolidario.Infra.Data;
using CabideSolidario.Domain.SolicitacaoDoacoes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using CabideSolidario.Endpoints.Security;
using System.Security.Claims;

namespace CabideSolidario.Endpoints.Solicitacao;

public class SolicitacaoDoacaoPost
{
    public static string Template => "/solicitacaodoacao";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    [Authorize(Policy = "DoadorPolicy")]
    public static IResult Action(SolicitacaoDoacaoRequest solicitacaoRequest, HttpContext http, UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        var solicitacao = new SolicitacaoDoacao
        {
            IdDoador = solicitacaoRequest.IdDoador,
            Instituicao = solicitacaoRequest.Instituicao,
            Status = solicitacaoRequest.Status,
            Quantidade = solicitacaoRequest.Quantidade,
            Tamanhos = solicitacaoRequest.Tamanhos,
            Condicao = solicitacaoRequest.Condicao,
            TipoPeca = solicitacaoRequest.TipoPeca,
            DisponivelParaEntrega = solicitacaoRequest.DisponívelParaEntrega,
            EditedBy = solicitacaoRequest.IdDoador
        };


        context.SolicitacaoDoacoes.Add(solicitacao);
        context.SaveChanges();

        return Results.Created($"/solicitacaodoacao/{solicitacao.Id}", solicitacao.Id);
    }
}
