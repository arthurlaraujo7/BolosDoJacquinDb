using BolosDoJacquinDb.BdContext;
using BolosDoJacquinDb.DTO;
using BolosDoJacquinDb.Interfaces;
using BolosDoJacquinDb.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinDb.Repositories;

public class AvaliacaoRepository : IAvaliacao
{
    private readonly BolosDoJacquinDbContext _context;

    public AvaliacaoRepository(BolosDoJacquinDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AvaliacaoDTO>> ObterTodosAsync()
    {
        return await _context.Set<Avaliacao>()
            .Select(a => new AvaliacaoDTO
            {
                Id = a.Id,
                Nota = a.Nota,
                Comentario = a.Comentario,
                Situacao = a.Situacao,
                MotivoOcultacao = a.MotivoOcultacao,
                DataCriacao = a.DataCriacao,
                DataAlteracao = a.DataAlteracao,
                UsuarioId = a.UsuarioId,
                ProdutoId = a.ProdutoId
            })
            .ToListAsync();
    }

    public async Task<AvaliacaoDTO?> ObterPorIdAsync(int id)
    {
        var a = await _context.Set<Avaliacao>().FindAsync(id);
        if (a == null) return null;

        return new AvaliacaoDTO
        {
            Id = a.Id,
            Nota = a.Nota,
            Comentario = a.Comentario,
            Situacao = a.Situacao,
            MotivoOcultacao = a.MotivoOcultacao,
            DataCriacao = a.DataCriacao,
            DataAlteracao = a.DataAlteracao,
            UsuarioId = a.UsuarioId,
            ProdutoId = a.ProdutoId
        };
    }

    public async Task<AvaliacaoDTO> CriarAsync(AvaliacaoDTO dto)
    {
        var entity = new Avaliacao
        {
            Nota = dto.Nota,
            Comentario = dto.Comentario,
            Situacao = dto.Situacao,
            MotivoOcultacao = dto.MotivoOcultacao,
            DataCriacao = DateTime.UtcNow,
            DataAlteracao = DateTime.UtcNow,
            UsuarioId = dto.UsuarioId,
            ProdutoId = dto.ProdutoId
        };

        await _context.Set<Avaliacao>().AddAsync(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        dto.DataCriacao = entity.DataCriacao;
        dto.DataAlteracao = entity.DataAlteracao;
        return dto;
    }

    public async Task<bool> AtualizarAsync(int id, AvaliacaoDTO dto)
    {
        var entity = await _context.Set<Avaliacao>().FindAsync(id);
        if (entity == null) return false;

        entity.Nota = dto.Nota;
        entity.Comentario = dto.Comentario;
        entity.Situacao = dto.Situacao;
        entity.MotivoOcultacao = dto.MotivoOcultacao;
        entity.DataAlteracao = DateTime.UtcNow;
        entity.UsuarioId = dto.UsuarioId;
        entity.ProdutoId = dto.ProdutoId;

        _context.Set<Avaliacao>().Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var entity = await _context.Set<Avaliacao>().FindAsync(id);
        if (entity == null) return false;

        _context.Set<Avaliacao>().Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}