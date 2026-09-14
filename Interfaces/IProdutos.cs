using BolosDoJacquinDb.DTO;

namespace BolosDoJacquinDb.Interfaces;

public interface IProdutos
{
    Task<IEnumerable<ProdutosDTO>> ObterTodosAsync();
    Task<ProdutosDTO?> ObterPorIdAsync(int id);
    Task<ProdutosDTO> CriarAsync(ProdutosDTO dto);
    Task<bool> AtualizarAsync(int id, ProdutosDTO dto);
    Task<bool> DeletarAsync(int id);
}