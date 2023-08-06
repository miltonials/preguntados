using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace preguntados.Models
{
    [Keyless]
    public partial class Vhistorialresultado
    {
        public string? Jugador { get; set; }
        public decimal? Aciertos { get; set; }
    }
}
