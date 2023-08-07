using MessagePack.Formatters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preguntados.Models;
using System.ComponentModel;

namespace preguntados.Controllers
{
    public class PreguntasController : Controller
    {
        private readonly preguntadosContext _context;

        public PreguntasController(preguntadosContext context)
        {
            _context = context;
        }

        // GET: Jugadores
        public async Task<IActionResult> Index(Jugadore jugador)
        {
            ViewData["player"] = jugador.Nombre;
            ViewBag.player = jugador.Nombre;

            ViewBag.preguntas = _context.Vpreguntasaleatorias.ToList();
            var preguntadosContext = _context.Historial;
            return View(await preguntadosContext.ToListAsync());
        }

        // POST: Jugadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre")] Jugadore jugadore)
        {
            if (ModelState.IsValid)
            {
                var result = await _context.Database.ExecuteSqlInterpolatedAsync($"CALL RegistrarJugador({jugadore.Nombre})");

                ViewBag.player = jugadore.Nombre.ToUpper();

                return RedirectToAction(nameof(Index),"Jugadores", jugadore);
            }
            return RedirectToAction(nameof(Index));
        }

        public int ValidarRespuesta(int pregunta, Historial sesion)
        {
            int respuesta = 1;
            //Task<int> respuesta = _context.Database.ExecuteSqlInterpolatedAsync($"CALL fValidarRespuesta({pregunta.Id}, {sesion.Id})");

            return respuesta == 1 ? 1 : 0;
        }
    }
}
