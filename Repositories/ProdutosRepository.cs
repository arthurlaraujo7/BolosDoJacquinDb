using BolosDoJacquinDb.BdContext;
using BolosDoJacquinDb.DTO;
using BolosDoJacquinDb.Interfaces;
using BolosDoJacquinDb.Models;
using Microsoft.EntityFrameworkCore;

namespace BolosDoJacquinDb.Repositories;

public class ProdutosRepository : IProdutos
{
    private readonly BolosDoJacquinDbContext _context;

    public ProdutosRepository(BolosDoJacquinDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProdutosDTO>> ObterTodosAsync()
    {
        return await _context.Produtos
            .Select(p => new ProdutosDTO
            {
                Id = p.Id,
                Nome = p.Nome,
                Preco = p.Preco,
                ImagemUrl = p.ImagemUrl,
                CategoriaId = p.CategoriaId,
                DescricaoCurta = p.DescricaoCurta,
                DescricaoLonga = p.DescricaoLonga,
                Disponibilidade = p.Disponibilidade,
                Situacao = p.Situacao
            })
            .ToListAsync();
    }

    public async Task<ProdutosDTO?> ObterPorIdAsync(int id)
    {
        var p = await _context.Produtos.FindAsync(id);
        if (p == null) return null;

        return new ProdutosDTO
        {
            Id = p.Id,
            Nome = p.Nome,
            Preco = p.Preco,
            ImagemUrl = p.ImagemUrl,
            CategoriaId = p.CategoriaId,
            DescricaoCurta = p.DescricaoCurta,
            DescricaoLonga = p.DescricaoLonga,
            Disponibilidade = p.Disponibilidade,
            Situacao = p.Situacao
        };
    }

    public async Task<ProdutosDTO> CriarAsync(ProdutosDTO dto)
    {
        var entity = new Produtos
        {
            Nome = dto.Nome,
            Preco = dto.Preco,
            ImagemUrl = dto.ImagemUrl,
            CategoriaId = dto.CategoriaId,
            DescricaoCurta = dto.DescricaoCurta,
            DescricaoLonga = dto.DescricaoLonga,
            Disponibilidade = dto.Disponibilidade,
            Situacao = dto.Situacao
        };

        await _context.Produtos.AddAsync(entity);
        await _context.SaveChangesAsync();

        dto.Id = entity.Id;
        return dto;
    }

    public async Task<bool> AtualizarAsync(int id, ProdutosDTO dto)
    {
        var entity = await _context.Produtos.FindAsync(id);
        if (entity == null) return false;

        entity.Nome = dto.Nome;
        entity.Preco = dto.Preco;
        entity.ImagemUrl = dto.ImagemUrl;
        entity.CategoriaId = dto.CategoriaId;
        entity.DescricaoCurta = dto.DescricaoCurta;
        entity.DescricaoLonga = dto.DescricaoLonga;
        entity.Disponibilidade = dto.Disponibilidade;
        entity.Situacao = dto.Situacao;

        _context.Produtos.Update(entity);
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<bool> DeletarAsync(int id)
    {
        var entity = await _context.Produtos.FindAsync(id);
        if (entity == null) return false;

        _context.Produtos.Remove(entity);
        return await _context.SaveChangesAsync() > 0;
    }
}