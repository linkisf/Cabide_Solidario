using CabideSolidario.Domain.Endereco;
using CabideSolidario.Domain.Inventario;
using CabideSolidario.Domain.SolicitacaoDoacoes;
using CabideSolidario.Domain.Usuario;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CabideSolidario.Infra.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{

    public DbSet<SolicitacaoDoacao> SolicitacaoDoacoes { get; set; }
    public DbSet<InventarioInstituicao> InventarioInstituicao { get; set; }
    public DbSet<EnderecoModel> Endereco{ get; set; }


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<EnderecoModel>()
            .ToTable("CAD_ENDERECO");
        builder.Entity<EnderecoModel>()
            .Property(p => p.Cep).IsRequired();
        builder.Entity<EnderecoModel>()
            .Property(p => p.Cidade).IsRequired();
        builder.Entity<EnderecoModel>()
            .Property(p => p.Bairro).IsRequired();
        builder.Entity<EnderecoModel>()
            .Property(p => p.Rua).IsRequired();



        builder.Entity<SolicitacaoDoacao>()
            .ToTable("CAD_SOLICITACAO_DOACAO");

        builder.Entity<InventarioInstituicao>()
            .ToTable("CAD_INVENTARIO_INSTITUICAO");

    }


    
}
