using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Username { get; set; } = null!;

        public byte[] Password { get; set; } = null!;

        public List<object>? Usuarios { get; set; }
    }
}
