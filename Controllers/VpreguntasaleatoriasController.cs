using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preguntados.Models;

namespace preguntados.Controllers
{
    public class VpreguntasaleatoriasController : Controller
    {
        private readonly preguntadosContext _context;

        public VpreguntasaleatoriasController(preguntadosContext context)
        {
            _context = context;
        }

        // GET: Resultadoes
        public async Task<IActionResult> Index()
        {
            //var preguntadosContext = _context.Vpreguntasaleatorias.Include(r => r.Pregunta).Include(r => r.OpcionA);
            //return View(await preguntadosContext.ToListAsync());
            return _context.Vpreguntasaleatorias != null ?
                View(await _context.Vpreguntasaleatorias.ToListAsync()) :
                Problem("Entity set 'preguntadosContext.Vpreguntasaleatorias'  is null.");
        }
    }
}
