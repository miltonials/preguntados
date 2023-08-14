using System;
using System.Collections.Generic;

namespace preguntados.Models
{
    /// <summary>
    /// Clase que modela la tabla preguntas de la base de datos
    /// </summary>
    public partial class Pregunta
    {
        public int Id { get; set; }
        public string? Enunciado { get; set; }
        public string OpcionA { get; set; } = null!;
        public string OpcionB { get; set; } = null!;
        public string OpcionC { get; set; } = null!;
    }
}
