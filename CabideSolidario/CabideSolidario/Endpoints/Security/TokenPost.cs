using CabideSolidario.Domain.Usuario;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CabideSolidario.Endpoints.Security;

public class TokenPost
{
    public static string Template => "/token";
    public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
    public static Delegate Handler => Action;

    [AllowAnonymous]
    public static IResult Action(LoginRequest loginRequest, UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        var user = userManager.FindByEmailAsync(loginRequest.Email).Result;

        if (user == null)
            return Results.BadRequest();

        if (!userManager.CheckPasswordAsync(user, loginRequest.Password).Result)
            return Results.BadRequest();

        var claims = userManager.GetClaimsAsync(user).Result;
        var subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Email, loginRequest.Email)
        });
        subject.AddClaims(claims);


        var key = Encoding.ASCII.GetBytes(configuration["JwtBaererTokenSettings:SecretKey"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = subject,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Audience = "CabideSolidario",
            Issuer = "CabideSolidarioIssuer",
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);


        return Results.Ok(new
        {
            token = tokenHandler.WriteToken(token)
        });

    }
}
