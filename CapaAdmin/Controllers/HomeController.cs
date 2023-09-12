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

        public async Task<JsonResult> ListarUsuarios()
        {
            var oUsuarios =  await _context.Listar();

            return Json(oUsuarios);
        }
    }
}