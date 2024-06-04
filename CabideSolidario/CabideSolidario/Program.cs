using CabideSolidario.Endpoints.Doadores;
using CabideSolidario.Endpoints.Solicitacao;
using CabideSolidario.Infra.Data;
using Microsoft.AspNetCore.Identity;

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

}).AddEntityFrameworkStores<ApplicationDbContext>();


var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.MapMethods(SolicitacaoDoacaoPost.Template, SolicitacaoDoacaoPost.Methods, SolicitacaoDoacaoPost.Handler);
app.MapMethods(SolicitacaoDoacaoGet.Template, SolicitacaoDoacaoGet.Methods, SolicitacaoDoacaoGet.Handler);
app.MapMethods(InstituicaoPost.Template, InstituicaoPost.Methods, InstituicaoPost.Handler);
app.MapMethods(DoadorPost.Template, DoadorPost.Methods, DoadorPost.Handler);
//app.MapMethods(DoadorGetAll.Template, DoadorGetAll.Methods, DoadorGetAll.Handler);

app.Run();
