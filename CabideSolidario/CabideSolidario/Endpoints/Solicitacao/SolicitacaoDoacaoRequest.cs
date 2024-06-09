namespace CabideSolidario.Endpoints.Solicitacao;

public record SolicitacaoDoacaoRequest(Guid IdDoador, Guid? Instituicao, string Status, int Quantidade, string Tamanhos, string Condicao, string TipoPeca, bool DisponívelParaEntrega);
