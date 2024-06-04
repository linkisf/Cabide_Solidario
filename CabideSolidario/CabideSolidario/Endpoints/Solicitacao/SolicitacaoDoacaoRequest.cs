namespace CabideSolidario.Endpoints.Solicitacao;

public record SolicitacaoDoacaoRequest(Guid IdDoador, int Quantidade, string Condicao, string TipoPeca, bool DisponivelParaEntrega, Guid? Instituicao, string Status);
