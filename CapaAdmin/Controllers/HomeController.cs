using CapaAdmin.Models;
using CapaAdmin.Models.DBEntidades;
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

        [HttpPost]
        public IActionResult GuardarUsuario(Usuario objeto)
        {
            object resultado;
            string mensaje = string.Empty;
            
            if(objeto.IdUsuario == 0) 
            {
                resultado = _context.Registrar(objeto, out mensaje);   
            }
            else 
            {
                resultado = _context.Editar(objeto, out mensaje);
            }

            var jsonResult = JsonConvert.SerializeObject(new { resultado = resultado,mensaje= mensaje }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

        [HttpDelete]
        public IActionResult EliminarUsuario(Usuario objeto)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            if (objeto.IdUsuario != 0)
            {
                resultado = _context.Delete(objeto.IdUsuario, out mensaje);
            }

            var jsonResult = JsonConvert.SerializeObject(new { resultado = resultado, mensaje = mensaje }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }
    }
}