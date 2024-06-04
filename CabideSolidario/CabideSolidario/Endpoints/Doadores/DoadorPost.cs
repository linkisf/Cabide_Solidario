using CabideSolidario.Domain.Usuario;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CabideSolidario.Endpoints.Doadores;

public class DoadorPost
{
    public static string Template => "/doador";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(DoadorUsuario doadorRequest, UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        //Registra novo usuario
        var user = new IdentityUser
        {
            UserName = doadorRequest.Email,
            Email = doadorRequest.Email,

        };
        var result = userManager.CreateAsync(user, doadorRequest.Password).Result;

        if(!result.Succeeded) 
            return Results.BadRequest(result.Errors.First());


        if (doadorRequest.Endereco != null)
        {
            var endereco = doadorRequest.Endereco;
            endereco.IdUsuario = Guid.Parse(user.Id);

            context.Endereco.AddAsync(endereco);
            context.SaveChangesAsync();
        }            

        var userClaims = new List<Claim>
        {
            new Claim("Nome", doadorRequest.Nome),
            new Claim("CPF", doadorRequest.CPF),
            new Claim("Telefone", doadorRequest.Telefone)
        };

        var claimResult = userManager.AddClaimsAsync(user, userClaims).Result;

        if(!claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());


        return Results.Created($"/doador/{user.Id}", user.Id);
    }
}
