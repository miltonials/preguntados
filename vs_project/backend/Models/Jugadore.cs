using System;
using System.Collections.Generic;
using preguntados.Models;

namespace preguntados_backend.Models
{
    public partial class Jugadore
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
