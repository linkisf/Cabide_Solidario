using CabideSolidario.Domain.Inventario;
using CabideSolidario.Domain.Usuario;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CabideSolidario.Endpoints.Inventario;

public class InventarioExecutarSolicitacao
{
    public static string Template => "/inventario/executasolicitacao/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    [Authorize(Policy="InstituicaoPolicy")]
    public async static Task<IResult> Action([FromRoute]Guid id, ApplicationDbContext context)
    {
        InventarioInstituicao? itemInventario = context.InventarioInstituicao.Where(i  => i.Id == id).FirstOrDefault();
        itemInventario.Executada = true;
       // await context.InventarioInstituicao.Update(itemInventario);
        await context.SaveChangesAsync();
        return Results.Ok("Solicitação Executada");
    }
}
