using System;
using System.Collections.Generic;

namespace preguntados.Models
{
    public partial class Jugadore
    {
        public Jugadore()
        {
            Resultados = new HashSet<Resultado>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; } = null!;

        public virtual ICollection<Resultado> Resultados { get; set; }
    }
}
