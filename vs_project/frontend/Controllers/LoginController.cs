using Microsoft.AspNetCore.Mvc;
using preguntados_frontend.Models;
using System.Diagnostics;

namespace preguntados_frontend.Controllers
{
    /// <summary>
    /// Clase que controla la navegación de la vista de login.
    /// </summary>
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Método que retorna la vista del login.
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Método error que se encarga de mostrar la vista de error.
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}