using CabideSolidario.Endpoints.Doadores;
using CabideSolidario.Endpoints.Instituicoes;
using CabideSolidario.Endpoints.Inventario;
using CabideSolidario.Endpoints.Security;
using CabideSolidario.Endpoints.Solicitacao;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//SQL como serviço para o entityFramework
builder.Services.AddSqlServer<ApplicationDbContext>(
    builder.Configuration["ConnectionStrings:CabideSolidario"]);

//Identity como serviço para gerenciamento de usuarios
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 3;

    options.User.RequireUniqueEmail = true;

    //options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ";

}).AddEntityFrameworkStores<ApplicationDbContext>();


//Configuração de autorização para o login
builder.Services.AddAuthorization(options =>           //adiciona serviço de autorização para saber quem está navegando pelo sistema
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()       //aplica a proteção obrigatoria para todas as rotas
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build();
    //Adicionando políticas para usuarios específicos    
    options.AddPolicy("DoadorPolicy", p =>
        p.RequireAuthenticatedUser().RequireClaim("UsuarioCode", "1"));
    options.AddPolicy("InstituicaoPolicy", p =>
        p.RequireAuthenticatedUser().RequireClaim("UsuarioCode", "2"));

});

//Autenticação
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,          //bonus de tempo para utilizar o token após sua expiração
        ValidIssuer = builder.Configuration["JwtBaererTokenSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtBaererTokenSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtBaererTokenSettings:SecretKey"]))
    };
});

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();


//ENDPOINTS: DOADORES
app.MapMethods(DoadorPost.Template, DoadorPost.Methods, DoadorPost.Handler);
app.MapMethods(DoadorGetAll.Template, DoadorGetAll.Methods, DoadorGetAll.Handler);
app.MapMethods(DoadorGet.Template, DoadorGet.Methods, DoadorGet.Handler);
app.MapMethods(DoadorPut.Template, DoadorPut.Methods, DoadorPut.Handler);

//ENDPOINTS: INSTITUICOES
app.MapMethods(InstituicaoPost.Template, InstituicaoPost.Methods, InstituicaoPost.Handler);
app.MapMethods(InstituicaoGet.Template, InstituicaoGet.Methods, InstituicaoGet.Handler);
app.MapMethods(InstituicaoGetAll.Template, InstituicaoGetAll.Methods, InstituicaoGetAll.Handler);
app.MapMethods(InstituicaoPut.Template, InstituicaoPut.Methods, InstituicaoPut.Handler);

//ENDPOINTS: ENDERECO


//ENDPOINTS: SOLICITACAO DOACAO
app.MapMethods(SolicitacaoDoacaoPost.Template, SolicitacaoDoacaoPost.Methods, SolicitacaoDoacaoPost.Handler);
app.MapMethods(SolicitacaoDoacaoGet.Template, SolicitacaoDoacaoGet.Methods, SolicitacaoDoacaoGet.Handler);
app.MapMethods(SolicitacaoDoacaoGetAll.Template, SolicitacaoDoacaoGetAll.Methods, SolicitacaoDoacaoGetAll.Handler);
app.MapMethods(SolicitacaoDoacaoPutAlterarStatus.Template, SolicitacaoDoacaoPutAlterarStatus.Methods, SolicitacaoDoacaoPutAlterarStatus.Handler);


//ENDPOINTS: INVENTARIO
app.MapMethods(InventarioPost.Template, InventarioPost.Methods, InventarioPost.Handler);
app.MapMethods(InventarioExecutarSolicitacao.Template, InventarioExecutarSolicitacao.Methods, InventarioExecutarSolicitacao.Handler);





//ENDPOINTS: TOKEN
app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handler);





//app.MapMethods(SolicitacaoDoacaoPost.Template, SolicitacaoDoacaoPost.Methods, SolicitacaoDoacaoPost.Handler);
//app.MapMethods(SolicitacaoDoacaoGet.Template, SolicitacaoDoacaoGet.Methods, SolicitacaoDoacaoGet.Handler);

//app.MapMethods(DoadorGetAll.Template, DoadorGetAll.Methods, DoadorGetAll.Handler);

app.Run();
