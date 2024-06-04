using CabideSolidario.Domain.Endereco;

namespace CabideSolidario.Domain.Usuario;

public class Usuario
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Telefone { get; set; }
    public string Password { get; set; }
    public EnderecoModel? Endereco { get; set; }

    public Usuario() 
    { 
        
    }

}
