using CabideSolidario.Domain.Usuario;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CabideSolidario.Endpoints.Doadores;

public class DoadorPost
{
    public static string Template => "/doador";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    [AllowAnonymous]
    public async static Task<IResult> Action(DoadorUsuario doadorRequest, UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        //Registra novo usuario
        var user = new IdentityUser
        {
            UserName = doadorRequest.Email,
            Email = doadorRequest.Email,
            PhoneNumber = doadorRequest.Telefone
        };

        var result = await userManager.CreateAsync(user, doadorRequest.Password);
        
        if (!result.Succeeded) 
            return Results.BadRequest(result.Errors.First());

        //Salva Endereco
        if (doadorRequest.Endereco != null)
        {
            var endereco = doadorRequest.Endereco;
            endereco.IdUsuario = Guid.Parse(user.Id);

            await context.Endereco.AddAsync(endereco);
            await context.SaveChangesAsync();
        }

        var userClaims = new List<Claim>
        {
            new Claim("Nome", doadorRequest.Nome),
            new Claim("CPF", doadorRequest.CPF),
            new Claim("UsuarioCode", "1")
        };

        var claimResult = await userManager.AddClaimsAsync(user, userClaims);

        if(!claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());


        return Results.Created($"/doador/{user.Id}", user.Id);
    }
}
