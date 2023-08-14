using preguntados_backend.Models;

namespace preguntados.Models
{
    /// <summary>
    /// Esta clase modela la tabla historial de la base de datos
    /// </summary>
    public partial class Historial
    {
        public string? Id { get; set; }
        public int JugadorId { get; set; }
        public int? Aciertos { get; set; }
        public DateTime FechaHora{ get; set; }
    }

}
