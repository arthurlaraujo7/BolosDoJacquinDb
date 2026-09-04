using System;
using System.Collections.Generic;

namespace BolosDoJacquinDb.Models;

public partial class Avaliacao
{
    public int Id { get; set; }

    public int Nota { get; set; }

    public string? Comentario { get; set; }

    public int Situacao { get; set; }

    public string? MotivoOcultacao { get; set; }

    public DateTime DataCriacao { get; set; }

    public DateTime DataAlteracao { get; set; }

    public Guid UsuarioId { get; set; }

    public int ProdutoId { get; set; }

    public virtual Produtos Produto { get; set; } = null!;

    public virtual Usuarios Usuario { get; set; } = null!;
}
