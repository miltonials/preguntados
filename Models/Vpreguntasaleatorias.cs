using Microsoft.EntityFrameworkCore;

namespace preguntados.Models
{
    [Keyless]
    public partial class Vpreguntasaleatorias
    {
        public int? Id { get; set; }
        public string? Pregunta { get; set; }
        public string OpcionA { get; set; } = null!;
        public string OpcionB { get; set; } = null!;
        public string OpcionC { get; set; } = null!;
        //public string RespuestaCorrecta { get; set; } = null!;
    }
}