using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preguntados.Models;
using preguntados_backend.Models;

namespace preguntados.Controllers
{
    /// <summary>
    /// Esta clase es el controlador de la aplicación
    /// </summary>
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
        // <summary>
        // Método que permite crear sesiones de juego
        // </summary>
        // <param name="nombreJugador">Nombre del jugador</param>
        // <returns>Objeto Sesion</returns>
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


        /// <summary>
        /// Método que permite validar la respuesta de una pregunta
        /// </summary>
        /// <param name="idPregunta"></param>
        /// <param name="respuesta"></param>
        /// <param name="sesion"></param>
        /// <returns>
        /// Este método retorna un entero(-1) que únicamente indica si se ejecutó correctamente la función
        /// </returns>
        [HttpGet("ValidarRespuesta")]
        public async Task<int> ValidarRespuesta(int idPregunta, char respuesta, string sesion)
        {
            var serverResponse = await _context.Database.ExecuteSqlInterpolatedAsync($"SELECT fValidarRespuesta({idPregunta}, {respuesta}, {sesion})");
            return serverResponse;
        }

        /// <summary>
        /// Método que permite obtener el historial de los jugadores
        /// </summary>
        /// <returns>
        /// Este método retorna una lista de objetos Historial ordenados descendentemente por el número de aciertos
        /// </returns>
        [HttpGet("historial")]
        public ActionResult<Historial> historial()
        {
            IEnumerable<Historial> resultado = _context.Historial.OrderByDescending(h => h.Aciertos).ToList();

            return Ok(resultado);
        }

        /// <summary>
        /// Método que permite obtener el puntaje de una sesión
        /// </summary>
        /// <param name="idSesion"></param>
        /// <returns>
        /// Retorna un entero que representa el número de aciertos de la sesión
        /// </returns>
        [HttpGet("ObtenerPuntaje")]
        public async Task<int> ObtenerPuntaje(string idSesion)
        {
            try
            {
                int aciertos = (int)_context.Historial.FirstOrDefault(h => h.Id == idSesion).Aciertos;
                return await Task.FromResult(aciertos);
            }
            catch (Exception)
            {
                return await Task.FromResult(-1);
            }
        }
    }
}
