using System;
using System.Collections.Generic;

namespace preguntados.Models
{
    public partial class Pregunta
    {
        public Pregunta()
        {
            Resultados = new HashSet<Resultado>();
        }

        public int Id { get; set; }
        public string Pregunta1 { get; set; } = null!;
        public string OpcionA { get; set; } = null!;
        public string OpcionB { get; set; } = null!;
        public string OpcionC { get; set; } = null!;
        public string RespuestaCorrecta { get; set; } = null!;

        public virtual ICollection<Resultado> Resultados { get; set; }
    }
}
