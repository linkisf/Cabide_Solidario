using CabideSolidario.Domain.Usuario;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace CabideSolidario.Endpoints.Doadores;

public class DoadorGetAll
{
    public static string Template => "/doador";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handler => Action;

    public static IResult Action(int page, int rows, IConfiguration configuration)
    {
        var db = new SqlConnection(configuration["ConnectionStrings:CabideSolidario"]);

        var query =
            @"select 
				c.UserId as Id
				,MAX(CASE WHEN c.ClaimType='CPF' THEN c.ClaimValue ELSE null end) AS CPF
				,MAX(CASE WHEN c.ClaimType='Nome' THEN c.ClaimValue ELSE null end) AS Nome
				,MAX(CASE WHEN c.ClaimType='Telefone' THEN c.ClaimValue ELSE null end) AS Telefone
				,u.Email
				,e.Cep as Cep
			from 
				AspNetUsers u 
				inner join AspNetUserClaims c on u.id = c.UserId 
				inner join CAD_ENDERECO e on e.IdUsuario = u.Id
			where 
				c.UserId in (select UserId from AspNetUserClaims where ClaimType = 'CPF')
				and e.Ativo = 1
			group by 
				c.UserId
				,u.Email
				,e.Cep
			order by Nome
			OFFSET (@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

        var doadores = db.QueryAsync<DoadorUsuario>(query, new { page, rows });
			


        return Results.Ok(doadores);


    }
}
