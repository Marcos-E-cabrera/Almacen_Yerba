using CapaAdmin.Models;
using CapaAdmin.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text.Json;

namespace CapaAdmin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsuarios _context;

        public HomeController(IUsuarios context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Usuarios()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios()
        {
            var oUsuarios = await _context.Listar();

            // Serializa los datos y aplica formato para hacerlos legibles
            var jsonResult = JsonConvert.SerializeObject(new { data = oUsuarios }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }
    }
}