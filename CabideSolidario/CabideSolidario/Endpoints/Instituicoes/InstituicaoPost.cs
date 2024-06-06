using CabideSolidario.Domain.Usuario;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace CabideSolidario.Endpoints.Instituicoes;

public class InstituicaoPost
{
    public static string Template => "/instituicao";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(InstituicaoUsuario instituicaoRequest, UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        //Registra novo usuario
        var user = new IdentityUser
        {
            UserName = instituicaoRequest.Email,
            Email = instituicaoRequest.Email,

        };
        var result = userManager.CreateAsync(user, instituicaoRequest.Password).Result;

        if (!result.Succeeded)
            return Results.BadRequest(result.Errors.First());

        //Salva Endereco
        if (instituicaoRequest.Endereco != null)
        {
            var endereco = instituicaoRequest.Endereco;
            endereco.IdUsuario = Guid.Parse(user.Id);

            context.Endereco.AddAsync(endereco);
            context.SaveChangesAsync();
        }

        var userClaims = new List<Claim>
        {
            new Claim("Nome", instituicaoRequest.Nome),
            new Claim("CNPJ", instituicaoRequest.CNPJ),
            new Claim("Telefone", instituicaoRequest.Telefone),
            new Claim("UsuarioCode", "2")
        };

        var claimResult = userManager.AddClaimsAsync(user, userClaims).Result;

        if (!claimResult.Succeeded)
            return Results.BadRequest(claimResult.Errors.First());


        return Results.Created($"/instituicao/{user.Id}", user.Id);
    }
}
