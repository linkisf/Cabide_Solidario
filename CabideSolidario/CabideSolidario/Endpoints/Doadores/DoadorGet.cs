﻿using CabideSolidario.Domain.Endereco;
using CabideSolidario.Domain.Usuario;
using CabideSolidario.Infra.Data;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace CabideSolidario.Endpoints.Doadores;

public class DoadorGet
{
    public static string Template => "/doador/{id:guid}";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

	[Authorize(Policy = "DoadorPolicy")]
    public static IResult Action([FromRoute] Guid id, UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {        
		var user = userManager.Users.Where(u => u.Id == id.ToString()).FirstOrDefault();
        
        if (user == null)
            return Results.BadRequest("Usuário não encontrado.");

        var claims = userManager.GetClaimsAsync(user).Result;

        EnderecoModel? endereco = context.Endereco.Where(u => u.IdUsuario == id && u.Ativo == true).FirstOrDefault();

        DoadorUsuario doador = new DoadorUsuario
        {
            Nome = user.UserName,
            Email = user.Email,
            Telefone = user.PhoneNumber,
            CPF = claims != null ? claims.FirstOrDefault(c => c.Type == "CPF").ToString() : "CPF não encontrado.",
            Endereco = endereco
        };

        
        return Results.Ok(doador);


    }
}
