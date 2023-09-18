using CapaAdmin.Models.DBEntidades;
using CapaAdmin.Models.Services.SerCategoria;
using CapaAdmin.Models.Services.SerUsuarios;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CapaAdmin.Controllers
{
    public class MantenimientoController : Controller
    {
        private readonly ICategoria _context;

        public MantenimientoController(ICategoria context)
        {
            _context = context;
        }

        public IActionResult Categorias()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarCategoria()
        {
            var oCategoria = await _context.Listar();

            // Serializa los datos y aplica formato para hacerlos legibles
            var jsonResult = JsonConvert.SerializeObject(new { data = oCategoria }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

        [HttpPost]
        public IActionResult GuardarCategoria(Models.DBEntidades.Categoria objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdCategoria == 0)
            {
                resultado = _context.Registrar(objeto, out mensaje);
            }
            else
            {
                resultado = _context.Editar(objeto, out mensaje);
            }

            var jsonResult = JsonConvert.SerializeObject(new { resultado = resultado, mensaje = mensaje }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

        [HttpDelete]
        public IActionResult EliminarCategoria(Models.DBEntidades.Categoria objeto)
        {
            bool resultado = false;
            string mensaje = string.Empty;

            if (objeto.IdCategoria != 0)
            {
                resultado = _context.Delete(objeto.IdCategoria, out mensaje);
            }

            var jsonResult = JsonConvert.SerializeObject(new { resultado = resultado, mensaje = mensaje }, Formatting.Indented);

            return Content(jsonResult, "application/json");
        }

        public IActionResult Marcas()
        {
            return View();
        }

        public IActionResult Productos()
        {
            return View();
        }
    }
}
