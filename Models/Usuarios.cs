using System;
using System.Collections.Generic;

namespace BolosDoJacquinDb.Models;

public partial class Usuarios
{
    public Guid Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string SenhaHash { get; set; } = null!;

    public int TipoUsuarioId { get; set; }

    public int Situacao { get; set; }

    public DateTime DataCadastro { get; set; }

    public virtual ICollection<Avaliacao> Avaliacos { get; set; } = new List<Avaliacao>();

    public virtual TipoUsuario TipoUsuario { get; set; } = null!;
}
