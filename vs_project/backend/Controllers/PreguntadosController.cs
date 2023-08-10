using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preguntados.Models;
using preguntados_backend.Models;

namespace preguntados.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreguntadosController : Controller
    {
        private readonly preguntadosContext _context;

        public PreguntadosController(preguntadosContext context)
        {
            _context = context;
        }

        // GET: Jugadores
        [HttpGet("CrearSesion")]
        public async Task<ActionResult<Sesion>> CrearSesion(string nombreJugador)
        {
            await _context.Database.ExecuteSqlInterpolatedAsync($"CALL RegistrarJugador({nombreJugador})");
            Jugadore jugador = _context.Jugadores.FirstOrDefault(j => j.Nombre == nombreJugador);
            Historial historial = _context.Historial.FirstOrDefault(h => h.JugadorId == jugador.Id && h.Aciertos == null);
            IEnumerable<Pregunta> preguntas = _context.Preguntas.ToList();

            var sesion = new Sesion
            {
                jugador = jugador,
                registro = historial,
                preguntas = preguntas
            };

            return Ok(sesion);
        }


        [HttpGet("ValidarRespuesta")]
        public async Task<int> ValidarRespuesta(int idPregunta, char respuesta, string sesion)
        {
            var serverResponse = await _context.Database.ExecuteSqlInterpolatedAsync($"SELECT fValidarRespuesta({idPregunta}, {respuesta}, {sesion})");
            return serverResponse;
        }

        [HttpGet("historial")]
        public ActionResult<Historial> historial()
        {
            IEnumerable<Historial> resultado = _context.Historial.OrderByDescending(h => h.Aciertos).ToList();

            return Ok(resultado);
        }
    }
}
