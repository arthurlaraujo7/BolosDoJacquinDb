using BolosDoJacquinDb.DTO;

namespace BolosDoJacquinDb.Interfaces;

public interface ITipoUsuario
{
    Task<IEnumerable<TipoUsuarioDTO>> ObterTodosAsync();
    Task<TipoUsuarioDTO?> ObterPorIdAsync(int id);
    Task<TipoUsuarioDTO> CriarAsync(TipoUsuarioDTO dto);
    Task<bool> AtualizarAsync(int id, TipoUsuarioDTO dto);
    Task<bool> DeletarAsync(int id);
}