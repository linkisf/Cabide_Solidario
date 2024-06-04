using CabideSolidario.Domain.Usuario;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace CabideSolidario.Endpoints.Doadores;

public class DoadorGetAll
{
    public static string Template => "/doador";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    //public static Delegate Handler => Action;

    //public static IResult Action(int page, int rows, IConfiguration configuration)
    //{
    //    var db = new SqlConnection(configuration["ConnectionStrings:CabideSolidario"]);

    //    var doadores = db.Query<DoadorUsuario>(
    //        @"select Email, ClaimValue as Nome
    //        from AspNetUsers u
    //        inner join 
    //        join AspNetUserClaims c
    //        on u.id = c.UserId and claimtype = 'Nome'"
    //    );


    //}
}
