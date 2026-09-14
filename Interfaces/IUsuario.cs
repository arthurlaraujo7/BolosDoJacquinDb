using BolosDoJacquinDb.DTO;

namespace BolosDoJacquinDb.Interfaces;

public interface IUsuario
{
    Task<IEnumerable<UsuariosDTO>> ObterTodosAsync();
    Task<UsuariosDTO?> ObterPorIdAsync(Guid id);
    Task<UsuariosDTO?> BuscarPorEmailESenhaAsync(string email, string senha);
    Task<UsuariosDTO> CriarAsync(UsuariosDTO dto);
    Task<bool> AtualizarAsync(Guid id, UsuariosDTO dto);
    Task<bool> DeletarAsync(Guid id);
}