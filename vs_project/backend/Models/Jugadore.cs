using System;
using System.Collections.Generic;
using preguntados.Models;

namespace preguntados_backend.Models
{
    /// <summary>
    /// Clase que modela la tabla jugadores de la base de datos
    /// </summary>
    public partial class Jugadore
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
    }
}
