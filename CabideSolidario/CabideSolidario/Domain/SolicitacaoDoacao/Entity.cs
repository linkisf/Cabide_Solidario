namespace CabideSolidario.Domain.SolicitacaoDoacao;

public abstract class Entity
{
    public Guid Id { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public Guid EditedBy { get; set; }

    public Entity()
    {
        Id = new Guid();
    }

    
}
