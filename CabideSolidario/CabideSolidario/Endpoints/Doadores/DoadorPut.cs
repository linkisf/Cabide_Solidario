using CabideSolidario.Domain.Usuario;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace CabideSolidario.Endpoints.Doadores;

public class DoadorPut
{
    public static string Template => "/doador/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
    public static Delegate Handler => Action;

    [Authorize(Policy = "DoadorPolicy")]    
    public static IResult Action([FromRoute]Guid id, DoadorUsuario doadorRequest, UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {

        var user = userManager.Users.Where(u => u.Id == id.ToString()).FirstOrDefault();

        user.UserName = doadorRequest.Nome;
        user.Email = doadorRequest.Email;
        user.PhoneNumber = doadorRequest.CPF;
        
        var result = userManager.UpdateAsync(user).Result;

        if (!result.Succeeded)
            return Results.BadRequest(result.Errors.First());                                    


        return Results.Ok();

    }
}
