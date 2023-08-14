using Microsoft.AspNetCore.Mvc;
using preguntados_frontend.Models;
using System.Diagnostics;
using Newtonsoft.Json.Linq;

namespace preguntados_frontend.Controllers
{
    /// <summary>
    /// Clase que modela el controlador del home.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Clase que se encarga de comunicarse con el backend.
        /// </summary>
        /// <param name="nombreJugador"></param>
        /// <returns>IActionResult: vista index del home</returns>
        [HttpPost]
        public IActionResult Index(string nombreJugador)
        {
            using (var client = new HttpClient())
            {
                string apiUrl = "https://localhost:7081/Preguntados";

                var sesion_response = client.GetAsync($"{apiUrl}/CrearSesion?nombreJugador={nombreJugador}").Result;
                string content_sesion_response = sesion_response.Content.ReadAsStringAsync().Result;
                JObject sesion = JObject.Parse(content_sesion_response);

                var history_response = client.GetAsync($"{apiUrl}/Historial").Result;
                string content_history_response = "{ \"resultados\":"
                    + history_response.Content.ReadAsStringAsync().Result
                    + "}";
                JObject historial = JObject.Parse(content_history_response);

                ViewBag.Sesion = sesion;
                ViewBag.Historial = historial;

            }   
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