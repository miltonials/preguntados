using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace preguntados.Models
{
    [Keyless]
    public partial class Vpreguntasaleatoria
    {
        public int? Id { get; set; }
        public string? Pregunta { get; set; }
        public string OpcionA { get; set; } = null!;
        public string OpcionB { get; set; } = null!;
        public string OpcionC { get; set; } = null!;
        //public string RespuestaCorrecta { get; set; } = null!;
    }
}
