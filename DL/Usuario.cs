using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string Username { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public virtual ICollection<Historial> Historials { get; set; } = new List<Historial>();
}
