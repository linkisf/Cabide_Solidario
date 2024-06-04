namespace CabideSolidario.Domain.SolicitacaoDoacao;

public class SolicitacaoDoacao : Entity
{    
    public Guid IdDoador { get; set; }    
    public int Quantidade { get; set; }
    //public List<string> Tamanho { get; set; }
    public string Condicao { get; set; }
    public string TipoPeca { get; set; }
    public bool DisponivelParaEntrega { get; set; }
    public Guid? Instituicao { get; set; }
    public string Status { get; set; }
    
}
