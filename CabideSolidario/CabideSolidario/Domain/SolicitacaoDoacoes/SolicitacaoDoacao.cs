namespace CabideSolidario.Domain.SolicitacaoDoacoes;

public class SolicitacaoDoacao
{
    public Guid Id { get; set; }    
    public Guid IdDoador { get; set; }
    public string Status { get; set; }
    public int Quantidade { get; set; }
    public string Tamanhos { get; set; }
    public string Condicao { get; set; }
    public string TipoPeca { get; set; }
    public bool DisponivelParaEntrega { get; set; }
    public Guid? Instituicao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAtualizacao { get; set; }
    public Guid EditedBy { get; set; }    

}
