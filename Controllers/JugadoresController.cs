using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preguntados.Models;

namespace preguntados.Controllers
{
    public class JugadoresController : Controller
    {
        private readonly preguntadosContext _context;

        public JugadoresController(preguntadosContext context)
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
    }
}
