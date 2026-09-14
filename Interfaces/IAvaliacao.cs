using BolosDoJacquinDb.DTO;

namespace BolosDoJacquinDb.Interfaces;

public interface IAvaliacao
{
    Task<IEnumerable<AvaliacaoDTO>> ObterTodosAsync();
    Task<AvaliacaoDTO?> ObterPorIdAsync(int id);
    Task<AvaliacaoDTO> CriarAsync(AvaliacaoDTO dto);
    Task<bool> AtualizarAsync(int id, AvaliacaoDTO dto);
    Task<bool> DeletarAsync(int id);
}