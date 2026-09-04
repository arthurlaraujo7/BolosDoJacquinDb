using System;
using System.Collections.Generic;

namespace BolosDoJacquinDb.Models;

public partial class TipoUsuario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Usuarios> Usuarios { get; set; } = new List<Usuarios>();
}
