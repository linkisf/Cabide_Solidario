using CabideSolidario.Domain.SolicitacaoDoacoes;

namespace CabideSolidario.Domain.Inventario;

public class InventarioInstituicao
{
    public Guid Id { get; set; }
    public Guid IdInstituicao { get; set; }
    public Guid IdSolicitacao { get; set; }
    public bool Executada { get; set; }
}
