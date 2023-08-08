namespace preguntados.Models
{
    public partial class Historial
    {
        public string? Id { get; set; }
        public int JugadorId { get; set; }
        public int? Aciertos { get; set; }
        public DateTime FechaHora{ get; set; }

        public virtual Jugadore Jugador { get; set; } = null!;
    }
}
