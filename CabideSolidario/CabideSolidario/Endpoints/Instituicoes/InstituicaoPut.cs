using CabideSolidario.Domain.Usuario;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace CabideSolidario.Endpoints.Instituicoes;

public class InstituicaoPut
{
    public static string Template => "/instituicao/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handler => Action;

    [Authorize(Policy = "InstituicaoPolicy")]    
    public static IResult Action([FromRoute]Guid id, InstituicaoUsuario doadorRequest, UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {

        var user = userManager.Users.Where(u => u.Id == id.ToString()).FirstOrDefault();

        if (user == null)
            return Results.BadRequest("Usuario não encontrado.");

        user.UserName = doadorRequest.Nome;
        user.Email = doadorRequest.Email;
        user.PhoneNumber = doadorRequest.CNPJ;
        
        var result = userManager.UpdateAsync(user).Result;

        if (!result.Succeeded)
            return Results.BadRequest(result.Errors.First());                                    


        return Results.Ok();

    }
}
