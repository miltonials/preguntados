using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            return _context.Jugadores != null ? 
                          View(await _context.Jugadores.ToListAsync()) :
                          Problem("Entity set 'preguntadosContext.Jugadores'  is null.");
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
                // ejecutar y guardar en una variable el resultado del procedimiento almacenado "CALL RegistrarJugador({jugadore.Nombre})"
                var result = await _context.Database.ExecuteSqlInterpolatedAsync($"CALL RegistrarJugador({jugadore.Nombre})");

                //mostrar el mensaje en la vista
                ViewData["player"] = jugadore.Nombre;
                ViewBag.player = jugadore.Nombre;

                return RedirectToAction(nameof(Index),"Jugadores", jugadore);
            }
            return RedirectToAction(nameof(Index));
        }

        private bool JugadoreExists(int id)
        {
          return (_context.Jugadores?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
