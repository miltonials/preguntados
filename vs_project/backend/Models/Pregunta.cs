﻿using System;
using System.Collections.Generic;

namespace preguntados.Models
{
    public partial class Pregunta
    {
        public int Id { get; set; }
        public string? Enunciado { get; set; }
        public string OpcionA { get; set; } = null!;
        public string OpcionB { get; set; } = null!;
        public string OpcionC { get; set; } = null!;
        //public string RespuestaCorrecta { get; set; } = null!;
    }
}