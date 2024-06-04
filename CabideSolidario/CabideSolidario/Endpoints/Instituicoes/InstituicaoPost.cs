using CabideSolidario.Domain.Usuario;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CabideSolidario.Endpoints.Doadores;

public class InstituicaoPost
{
    public static string Template => "/instituicao";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(DoadorUsuario doadorRequest, UserManager<IdentityUser> userManager)
    {
        var user = new IdentityUser
        {
            UserName = doadorRequest.Email,
            Email = doadorRequest.Email,

        };
        var result = userManager.CreateAsync(user, doadorRequest.Password).Result;

        if(!result.Succeeded) 
            return Results.BadRequest(result.Errors.First());

        var userClaims = new List<Claim>
        {
            new Claim("Nome", doadorRequest.Nome),
            new Claim("CPF", doadorRequest.CPF)
        };

        var claimResult = userManager.AddClaimsAsync(user, userClaims).Result;

        if(claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());


        return Results.Created($"/doador/{user.Id}", user.Id);
    }
}
