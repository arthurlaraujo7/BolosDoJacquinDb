using BolosDoJacquinDb.BdContext;
using BolosDoJacquinDb.DTO;
using BolosDoJacquinDb.Interfaces;
using BolosDoJacquinDb.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinDb.Repositories;

public class CategoriasRepository : ICategorias
{
    private readonly BolosDoJacquinDbContext _context;

    public CategoriasRepository(BolosDoJacquinDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CategoriasDTO>> ObterTodosAsync()
    {
        return await _context.Categorias
            .Select(c => new CategoriasDTO
            {
                Id = c.Id,
                Nome = c.Nome
            })
            .ToListAsync();
    }

    public async Task<CategoriasDTO?> ObterPorIdAsync(int id)
    {
        var entity = await _context.Categorias.FindAsync(id);
        if (entity == null) return null;

        return new CategoriasDTO
        {
            Id = entity.Id,
            Nome = entity.Nome
        };
    }

    public async Task<CategoriasDTO> CriarAsync(CategoriasDTO dto)
    {
        var entity = new Categorias
        {
            Nome = dto.Nome
        };

        await _context.Categorias.AddAsync(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        return dto;
    }

    public async Task<bool> AtualizarAsync(int id, CategoriasDTO dto)
    {
        var entity = await _context.Categorias.FindAsync(id);
        if (entity == null) return false;

        entity.Nome = dto.Nome;

        _context.Categorias.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var entity = await _context.Categorias.FindAsync(id);
        if (entity == null) return false;

        _context.Categorias.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}