namespace BolosDoJacquinDb.DTO;

public class UsuariosDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string SenhaHash { get; set; } = null!;
    public int TipoUsuarioId { get; set; }
    public int Situacao { get; set; }
    public DateTime DataCadastro { get; set; }
}