using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using preguntados.Models;
using System.Diagnostics;

namespace preguntados.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly preguntadosContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = new preguntadosContext();
        }

        public IActionResult Index()
        {
            //que retorne a la vista "Jugadores"
            preguntadosContext _context = new preguntadosContext();
            return View();
        }
    }
}