using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using preguntados.Models;

namespace preguntados.Controllers
{
    public class VhistorialresultadosController : Controller
    {
        private readonly preguntadosContext _context;

        public VhistorialresultadosController(preguntadosContext context)
        {
            _context = context;
        }

        // GET: Resultadoes
        public async Task<IActionResult> Index()
        {
            //var preguntadosContext = _context.Vhistorialresultados.Include(r => r.Jugador).Include(r => r.Aciertos);
            //return View(await preguntadosContext.ToListAsync());
            return _context.Vhistorialresultados != null ?
                View(await _context.Vhistorialresultados.ToListAsync()) :
                Problem("Entity set 'preguntadosContext.Preguntas'  is null.");
        }
    }
}
