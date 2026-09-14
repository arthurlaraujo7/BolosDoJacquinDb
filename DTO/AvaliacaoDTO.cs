namespace BolosDoJacquinDb.DTO;

public class AvaliacaoDTO
{
    public int Id { get; set; }
    public int Nota { get; set; }
    public string? Comentario { get; set; }
    public int Situacao { get; set; }
    public string? MotivoOcultacao { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime DataAlteracao { get; set; }
    public Guid UsuarioId { get; set; }
    public int ProdutoId { get; set; }
}