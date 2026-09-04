using System;
using System.Collections.Generic;

namespace BolosDoJacquinDb.Models;

public partial class Produtos
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public string ImagemUrl { get; set; } = null!;

    public int CategoriaId { get; set; }

    public string DescricaoCurta { get; set; } = null!;

    public string? DescricaoLonga { get; set; }

    public bool Disponibilidade { get; set; }

    public int Situacao { get; set; }

    public virtual ICollection<Avaliacao> Avaliacos { get; set; } = new List<Avaliacao>();

    public virtual Categorias Categoria { get; set; } = null!;
}
