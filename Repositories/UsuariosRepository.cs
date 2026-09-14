using BolosDoJacquinDb.BdContext;
using BolosDoJacquinDb.DTO;
using BolosDoJacquinDb.Interfaces;
using BolosDoJacquinDb.Models;
using BolosDoJacquinDb.Utils;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinDb.Repositories;

public class UsuariosRepository : IUsuario
{
    private readonly BolosDoJacquinDbContext _context;

    public UsuariosRepository(BolosDoJacquinDbContext context)
    {
        _context = context;
    }

    public async Task<UsuariosDTO?> BuscarPorEmailESenhaAsync(string email, string senha)
    {
        var usuario = await _context.Set<Usuarios>()
            .FirstOrDefaultAsync(u => u.Email == email);

        if (usuario == null) return null;

        string senhaHash = Criptografia.GerarHash(senha);
        if (usuario.SenhaHash != senhaHash) return null;

        return new UsuariosDTO
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            SenhaHash = usuario.SenhaHash,
            TipoUsuarioId = usuario.TipoUsuarioId,
            Situacao = usuario.Situacao,
            DataCadastro = usuario.DataCadastro
        };
    }

    public async Task<IEnumerable<UsuariosDTO>> ObterTodosAsync()
    {
        return await _context.Set<Usuarios>()
            .Select(u => new UsuariosDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                SenhaHash = u.SenhaHash,
                TipoUsuarioId = u.TipoUsuarioId,
                Situacao = u.Situacao,
                DataCadastro = u.DataCadastro
            })
            .ToListAsync();
    }

    public async Task<UsuariosDTO?> ObterPorIdAsync(Guid id)
    {
        var u = await _context.Set<Usuarios>().FindAsync(id);
        if (u == null) return null;

        return new UsuariosDTO
        {
            Id = u.Id,
            Nome = u.Nome,
            Email = u.Email,
            SenhaHash = u.SenhaHash,
            TipoUsuarioId = u.TipoUsuarioId,
            Situacao = u.Situacao,
            DataCadastro = u.DataCadastro
        };
    }

    public async Task<UsuariosDTO> CriarAsync(UsuariosDTO dto)
    {
        var entity = new Usuarios
        {
            Id = dto.Id == Guid.Empty ? Guid.NewGuid() : dto.Id,
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = Criptografia.GerarHash(dto.SenhaHash),
            TipoUsuarioId = dto.TipoUsuarioId,
            Situacao = dto.Situacao,
            DataCadastro = dto.DataCadastro == default ? DateTime.UtcNow : dto.DataCadastro
        };

        await _context.Set<Usuarios>().AddAsync(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        dto.DataCadastro = entity.DataCadastro;
        return dto;
    }

    public async Task<bool> AtualizarAsync(Guid id, UsuariosDTO dto)
    {
        var entity = await _context.Set<Usuarios>().FindAsync(id);
        if (entity == null) return false;

        entity.Nome = dto.Nome;
        entity.Email = dto.Email;
        entity.SenhaHash = Criptografia.GerarHash(dto.SenhaHash);
        entity.TipoUsuarioId = dto.TipoUsuarioId;
        entity.Situacao = dto.Situacao;

        _context.Set<Usuarios>().Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletarAsync(Guid id)
    {
        var entity = await _context.Set<Usuarios>().FindAsync(id);
        if (entity == null) return false;

        _context.Set<Usuarios>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}