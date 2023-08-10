using preguntados.Models;

namespace preguntados_backend.Models
{
    public partial class Sesion
    {
        public Historial? registro { get; set; }
        public Jugadore? jugador { get; set; }
        public IEnumerable<Pregunta>? preguntas { get; set; }
    }
}