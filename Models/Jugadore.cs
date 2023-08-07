using System;
using System.Collections.Generic;

namespace preguntados.Models
{
    public partial class Jugadore
    {
        public Jugadore()
        {
            Resultados = new HashSet<Historial>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Historial> Resultados { get; set; }
    }
}
