using System;
using System.Collections.Generic;

namespace BolosDoJacquinDb.Models;

public partial class Categorias
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Produtos> Produtos { get; set; } = new List<Produtos>();
}
