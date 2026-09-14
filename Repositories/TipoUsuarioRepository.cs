using BolosDoJacquinDb.BdContext;
using BolosDoJacquinDb.DTO;
using BolosDoJacquinDb.Interfaces;
using BolosDoJacquinDb.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinDb.Repositories;

public class TipoUsuarioRepository : ITipoUsuario
{
    private readonly BolosDoJacquinDbContext _context;

    public TipoUsuarioRepository(BolosDoJacquinDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TipoUsuarioDTO>> ObterTodosAsync()
    {
        return await _context.Set<TipoUsuario>()
            .Select(t => new TipoUsuarioDTO
            {
                Id = t.Id,
                Nome = t.Nome
            })
            .ToListAsync();
    }

    public async Task<TipoUsuarioDTO?> ObterPorIdAsync(int id)
    {
        var entity = await _context.Set<TipoUsuario>().FindAsync(id);
        if (entity == null) return null;

        return new TipoUsuarioDTO
        {
            Id = entity.Id,
            Nome = entity.Nome
        };
    }

    public async Task<TipoUsuarioDTO> CriarAsync(TipoUsuarioDTO dto)
    {
        var entity = new TipoUsuario
        {
            Nome = dto.Nome
        };

        await _context.Set<TipoUsuario>().AddAsync(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        return dto;
    }

    public async Task<bool> AtualizarAsync(int id, TipoUsuarioDTO dto)
    {
        var entity = await _context.Set<TipoUsuario>().FindAsync(id);
        if (entity == null) return false;

        entity.Nome = dto.Nome;

        _context.Set<TipoUsuario>().Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var entity = await _context.Set<TipoUsuario>().FindAsync(id);
        if (entity == null) return false;

        _context.Set<TipoUsuario>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}