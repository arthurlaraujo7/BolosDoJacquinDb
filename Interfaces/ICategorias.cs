using BolosDoJacquinDb.DTO;

namespace BolosDoJacquinDb.Interfaces;

public interface ICategorias
{
    Task<IEnumerable<CategoriasDTO>> ObterTodosAsync();
    Task<CategoriasDTO?> ObterPorIdAsync(int id);
    Task<CategoriasDTO> CriarAsync(CategoriasDTO dto);
    Task<bool> AtualizarAsync(int id, CategoriasDTO dto);
    Task<bool> DeletarAsync(int id);
}