namespace CabideSolidario.Domain.Endereco;

public class EnderecoModel
{
    public Guid Id { get; set; }
    public Guid IdUsuario { get; set; }
    public string Cep { get; set; }
    public string Cidade { get; set; }
    public string Bairro { get; set; }
    public string Rua { get; set; }
    public string Numero { get; set; }
    public string Complemento { get; set; }
    public bool Ativo { get; set; }

}
