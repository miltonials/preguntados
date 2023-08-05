using System;
using System.Collections.Generic;

namespace preguntados.Models
{
    public partial class Resultado
    {
        public int Id { get; set; }
        public int JugadorId { get; set; }
        public int PreguntaId { get; set; }
        public string RespuestaElegida { get; set; } = null!;
        public bool EsCorrecta { get; set; }
        public DateTime FechaHoraRespuesta { get; set; }

        public virtual Jugadore Jugador { get; set; } = null!;
        public virtual Pregunta Pregunta { get; set; } = null!;
    }
}
