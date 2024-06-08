using CabideSolidario.Domain.Inventario;
using CabideSolidario.Domain.Usuario;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CabideSolidario.Endpoints.Inventario;

public class InventarioPost
{
    public static string Template => "/inventario/capturasolicitacao";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    [Authorize(Policy="InstituicaoPolicy")]
    public async static Task<IResult> Action(InventarioInstituicao inventarioInstituicao, ApplicationDbContext context)
    {
        await context.InventarioInstituicao.AddAsync(inventarioInstituicao);
        await context.SaveChangesAsync();
        return Results.Ok(inventarioInstituicao.Id);
    }
}
